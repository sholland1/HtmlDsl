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
        [Fact]
        public void tag_produces_a_tag_with_text() {
            var expected = "<p>test</p>";
            Assert.Equal(expected, _p("test").Render());
            Assert.Equal(expected, _tag("p", "test").Render());
        }
        [Fact]
        public void tag_produces_an_empty_tag_with_attrs() {
            var expected = "<p foo=\"bar\" />";
            Assert.Equal(expected, _p(("foo", "bar")).Render());
            Assert.Equal(expected, _tag("p", ("foo", "bar")).Render());
        }
        [Fact]
        public void tag_produces_a_tag_with_children() {
            var expected = "<p><span /><span /></p>";
            Assert.Equal(expected, _p(_span(), _span()).Render());
            Assert.Equal(expected, _tag("p", _tag("span"), _tag("span")).Render());
        }
        [Fact]
        public void tag_produces_a_tag_and_attrs_with_children() {
            var expected = "<p foo=\"bar\"><span /><span /></p>";
            Assert.Equal(expected, _p(_(("foo", "bar")), _span(), _span()).Render());
            Assert.Equal(expected, _tag("p", _(("foo", "bar")), _tag("span"), _tag("span")).Render());
        }
    }
}
