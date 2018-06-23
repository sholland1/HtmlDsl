HtmlDsl
=======

This library is a dsl embedded in the C# language for generating HTML documents. It is inspired by the [Lucid](https://chrisdone.com/posts/lucid) library from Haskell. It is intended as an alternative to the ASP.NET Razor templating system.

Example usage:
```c#
using HtmlDsl;
using static HtmlDsl.Tags;
.
.
IHtml Evens(int n) =>
    _ul(Enumerable
            .Range(0, n)
            .Where(x => x % 2 == 0)
            .Select(x => _li($"{x}"))
            .ToArray());

string readmeExample =
    _html(
        _head(
            _title("HtmlDsl README")),
        _body(
            _comment("This is a comment."),
            _h1(("id", "myHeader"))("HtmlDsl"),
            _br(),
            _div(
                _div(("id", "inner-div"),
                     ("name", "inner"))
                    (_span("This library is ", _i("very"), " cool!")),
            Evens(10))))
    .Render();
```
Example output (formatted for readability):
```html
<html>
  <head>
    <title>HtmlDsl README</title>
  </head>
  <body>
    <!--This is a comment.-->
    <h1 id="myHeader">HtmlDsl</h1>
    <br />
    <div>
      <div id="inner-div" name="inner">
        <span>This library is <i>very</i> cool!</span>
      </div>
      <ul>
        <li>0</li>
        <li>2</li>
        <li>4</li>
        <li>6</li>
        <li>8</li>
      </ul>
    </div>
  </body>
</html>
```
