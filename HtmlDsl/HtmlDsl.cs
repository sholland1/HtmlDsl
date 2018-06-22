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

        public string Name { get; set; }
        public IEnumerable<(string name, string value)> Attributes { get; set; } = new(string, string)[] { };
        public IEnumerable<IHtml> Children { get; set; } = new IHtml[] { };

        public override StringBuilder RenderSB(StringBuilder sb) {
            var attrs = Attributes
                .Aggregate(sb.Append($"<{Name}"),
                           (acc, t) => acc.Append($" {t.name}=\"{t.value}\""));
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

        public string Content { get; set; }

        public override StringBuilder RenderSB(StringBuilder sb) => sb.Append($"<!--{Content}-->");
    }
    internal class RawHtml : IHtml {
        public RawHtml(string content) => Content = content;

        public string Content { get; set; }

        public override string Render() => Content;
        public override StringBuilder RenderSB(StringBuilder sb) => sb.Append(Content);
    }
    public static class HTMLHelpers {
        public delegate B ParamsFunc<A, B>(params A[] ps);

        public static IHtml _tag(string name) => new TagElement(name);

        public static IHtml _tag(string name, object obj) =>
            new TagElement(name) { Children = new[] { _text(obj) } };

        public static IHtml _tag(string name, params IHtml[] children) =>
            new TagElement(name) { Children = children };

        public static ParamsFunc<IHtml, IHtml> _tag(string name, params (string, string)[] attrs) =>
            new ParamsFunc<IHtml, IHtml>(
                children => new TagElement(name) { Attributes = attrs, Children = children });

        public static IHtml _text(object obj) =>
            new TextElement(obj.ToString());

        public static IHtml _comment() => new CommentElement();

        public static IHtml _comment(object content) =>
            new CommentElement(content.ToString().Replace("-->", "--\\>"));

        public static IHtml _raw(string s) => new RawHtml(s);
    }
    internal class TagInfo {
        public string Name { get; set; }
        public char Type { get; set; }
        public bool IsSingleton { get; set; }
    }
}
