using FsCheck;
using FsCheck.Xunit;
using System.Net;
using Xunit;
using static HtmlDsl.HTMLHelpers;
using static HtmlDsl.Tags;

namespace HtmlDsl.Tests {
    public class BasicTests {
        [Fact]
        public void tag_produces_an_empty_tag() {
            var expected = "<p />";
            Assert.Equal(expected, _p().Render());
            Assert.Equal(expected, _tag("p").Render());
        }
        [Property]
        public void tag_produces_a_tag_with_text(NonNull<string> s) {
            var expected = $"<p>{WebUtility.HtmlEncode(s.Get)}</p>";
            Assert.Equal(expected, _p(s.Get).Render());
            Assert.Equal(expected, _tag("p", s.Get).Render());
        }
        [Property]
        public void tag_produces_an_empty_tag_with_attrs(NonNull<string> name, NonNull<string> value) {
            var expected = $"<p {name.Get}=\"{value.Get}\" />";
            Assert.Equal(expected, _p((name.Get, value.Get)).Render());
            Assert.Equal(expected, _tag("p", (name.Get, value.Get)).Render());
        }
        [Fact]
        public void tag_produces_a_tag_with_children() {
            var expected = "<p><span /><span /></p>";
            Assert.Equal(expected, _p(_span(), _span()).Render());
            Assert.Equal(expected, _tag("p", _tag("span"), _tag("span")).Render());
        }
        [Property]
        public void tag_produces_a_tag_and_attrs_with_children(NonNull<string> name, NonNull<string> value) {
            var expected = $"<p {name.Get}=\"{value.Get}\"><span /><span /></p>";
            Assert.Equal(expected, _p(_((name.Get, value.Get)), _span(), _span()).Render());
            Assert.Equal(expected, _tag("p", _((name.Get, value.Get)), _tag("span"), _tag("span")).Render());
        }

        [Property]
        public void text_produces_an_string(NonNull<string> s) {
            Assert.Equal(WebUtility.HtmlEncode(s.Get), _text(s.Get).Render());
        }
        [Property]
        public void text_produces_ToString_of_object(NonNull<string> s) {
            var x = new Foo(s.Get);
            Assert.Equal(WebUtility.HtmlEncode(x.ToString()), _text(x).Render());
        }
        class Foo {
            private readonly string _value;
            public Foo(string val) => _value = val;
            public override string ToString() => _value;
        }

        [Fact]
        public void empty_comment_produces_an_empty_comment() {
            Assert.Equal("<!---->", _comment().Render());
        }
        [Property]
        public void comment_produces_a_comment(NonNull<string> s) {
            Assert.Equal($"<!--{s.Get.Replace("-->", "--\\>")}-->", _comment(s.Get).Render());
        }
    }
}
