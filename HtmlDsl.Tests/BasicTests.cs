using FsCheck;
using FsCheck.Xunit;
using System.Net;
using Xunit;
using static HtmlDsl.HTMLUtils;
using static HtmlDsl.Tags;
using static HtmlDsl.Attrs;
using System.Drawing;

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
        public void tag_produces_an_empty_tag_with_tuple_attrs(NonNull<string> name, NonNull<string> value) {
            var expected = $"<p {name.Get}=\"{value.Get}\" />";
            Assert.Equal(expected, _p((name.Get, value.Get))().Render());
            Assert.Equal(expected, _tag("p", (name.Get, value.Get))().Render());
        }
        [Property]
        public void tag_produces_an_empty_tag_with_attrs(NonNull<string> value) {
            var expected = $"<p id=\"{value.Get}\" hidden dir=\"rtl\" />";
            Assert.Equal(expected, _p(_id(value.Get), _hidden(), TextDir.rtl)().Render());
            Assert.Equal(expected, _tag("p", _attr("id", value.Get), _attr("hidden", true), _attr("dir", TextDir.rtl.ToString()))().Render());
        }
        [Fact]
        public void tag_produces_a_tag_with_children() {
            var expected = "<p><span /><span /></p>";
            Assert.Equal(expected, _p(_span(), _span()).Render());
            Assert.Equal(expected, _tag("p", _tag("span"), _tag("span")).Render());
        }
        [Property]
        public void tag_produces_a_tag_and_attrs_with_children(NonNull<string> value) {
            const string url = "http://www.google.com/";
            var expected = $"<a id=\"{value.Get}\" href=\"{url}\"><span /><span /></a>";
            Assert.Equal(expected, _a(_id(value.Get), _hidden(false), _href(new System.Uri(url)))(_span(), _span()).Render());
            Assert.Equal(expected, _tag("a", _attr("id", value.Get), _attr("hidden", false), _attr("href", url))(_tag("span"), _tag("span")).Render());
        }
        [Property]
        public void tag_produces_a_tag_and_tuple_attrs_with_children(NonNull<string> name, NonNull<string> value) {
            var expected = $"<p {name.Get}=\"{value.Get}\"><span /><span /></p>";
            Assert.Equal(expected, _p((name.Get, value.Get))(_span(), _span()).Render());
            Assert.Equal(expected, _tag("p", (name.Get, value.Get))(_tag("span"), _tag("span")).Render());
        }
        [Fact]
        public void weird_attrs_work() {
            Assert.Equal(" bgcolor=\"#ff0000\"", _bgcolor(Color.Red).Render());
            Assert.Equal(" datetime=\"2000-01-02T12:30:05.0050000\"", _datetime(new System.DateTime(2000, 1, 2, 12, 30, 5, 5)).Render());
            Assert.Equal(" datetime=\"1d2h3m4.0050000s\"", _datetime(new System.TimeSpan(1, 2, 3, 4, 5)).Render());
        }

        [Fact(Skip = "just checking for compile")]
        public void implicit_conversion_compiles() {
            _p(_span(), "test");
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
