using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HtmlDsl {
    public abstract class IHtml {
        public virtual string Render() => RenderSB(new StringBuilder()).ToString();
        public abstract StringBuilder RenderSB(StringBuilder sb);
        public static implicit operator IHtml(string text) => new TextElement(text);
    }
    internal class TextElement : IHtml {
        public TextElement(string text) => Text = WebUtility.HtmlEncode(text);
        public string Text { get; }

        public override string Render() => Text;
        public override StringBuilder RenderSB(StringBuilder sb) => sb.Append(Text);
    }
    internal class TagElement : IHtml {
        public TagElement(string name) => Name = name;

        public string Name { get; }
        public IEnumerable<IAttr> Attributes { get; set; } = new IAttr[] { };
        public IEnumerable<IHtml> Children { get; set; } = new IHtml[] { };

        public override StringBuilder RenderSB(StringBuilder sb) {
            var attrs = Attributes
                .Aggregate(sb.Append($"<{Name}"),
                           (acc, attr) => acc.Append(attr.Render()));
            return Children.Any()
                ? Children
                    .Aggregate(attrs.Append(">"),
                               (acc, h) => h.RenderSB(acc))
                    .Append($"</{Name}>")
                : attrs.Append(" />");
        }
    }
    internal class CommentElement : IHtml {
        public CommentElement() { }
        public CommentElement(string content) => Content = content;

        public string Content { get; }

        public override StringBuilder RenderSB(StringBuilder sb) => sb.Append($"<!--{Content}-->");
    }
    internal class RawHtml : IHtml {
        public RawHtml(string content) => Content = content;

        public string Content { get; }

        public override string Render() => Content;
        public override StringBuilder RenderSB(StringBuilder sb) => sb.Append(Content);
    }
    public abstract partial class IAttr {
        public abstract string Render();
        public static implicit operator IAttr((string name, string value) t) =>
            new StandardAttr(t.name, t.value);
    }
    internal class StandardAttr : IAttr {
        public StandardAttr(string name, string value) {
            Name = name;
            Value = value;
        }
        public string Name { get; }
        public string Value { get; }

        public override string Render() => $" {Name}=\"{Value}\"";
    }
    internal class BoolAttr : IAttr {
        public BoolAttr(string name, bool value) {
            Name = name;
            Value = value;
        }
        public string Name { get; }
        public bool Value { get; }

        public override string Render() => Value ? " " + Name : "";
    }
    public static class HTMLUtils {
        public delegate B ParamsFunc<A, B>(params A[] ps);

        public static IHtml _tag(string name) => new TagElement(name);

        public static IHtml _tag(string name, params IHtml[] children) =>
            new TagElement(name) { Children = children };

        public static ParamsFunc<IHtml, IHtml> _tag(string name, params IAttr[] attrs) =>
            new ParamsFunc<IHtml, IHtml>(
                children => new TagElement(name) { Attributes = attrs, Children = children });

        public static IAttr _attr(string name, bool value) => new BoolAttr(name, value);
        public static IAttr _attr(string name, string value) => new StandardAttr(name, value);


    }
}
