using System;

namespace HtmlDsl {
    public abstract partial class IAttr {
        public static implicit operator IAttr(Align value) => new StandardAttr("align", value.ToString());
        public static implicit operator IAttr(Autocapitalize value) => new StandardAttr("autocapitalize", value.ToString());
        public static implicit operator IAttr(CrossOrigin value) => new StandardAttr("crossorigin", value.ToString().Replace('_', '-'));
        public static implicit operator IAttr(TextDir value) => new StandardAttr("dir", value.ToString());
        public static implicit operator IAttr(DragDrop value) => new StandardAttr("dropzone", value.ToString());
        public static implicit operator IAttr(EncType value) => new StandardAttr("enctype", value.ToString());
        public static implicit operator IAttr(HttpEquiv value) => new StandardAttr("http-equiv", value.ToString().Replace('_', '-'));
        public static implicit operator IAttr(TrackKind value) => new StandardAttr("kind", value.ToString());
        public static implicit operator IAttr(HttpMethod value) => new StandardAttr("method", value.ToString());
        public static implicit operator IAttr(Preload value) => new StandardAttr("preload", value.ToString());
        public static implicit operator IAttr(Scope value) => new StandardAttr("scope", value.ToString());
        public static implicit operator IAttr(Shape value) => new StandardAttr("shape", value.ToString());
        public static implicit operator IAttr(Target value) => new StandardAttr("target", value.ToString());
        public static implicit operator IAttr(TextWrap value) => new StandardAttr("wrap", value.ToString());
        public static implicit operator IAttr(Valign value) => new StandardAttr("valign", value.ToString());

        public static implicit operator IAttr(ButtonType value) => new StandardAttr("type", value.ToString());
        public static implicit operator IAttr(InputType value) => new StandardAttr("type", value.ToString().Replace('_', '-'));
        public static implicit operator IAttr(MenuType value) => new StandardAttr("type", value.ToString());
    }
    [Obsolete("Not Supported In Html5")]
    public enum Align {
        left, right, center, justify
    }
    public enum Autocapitalize {
        off, none, on, sentences, words, characters
    }
    public enum CrossOrigin {
        anonymous, use_credentials
    }
    public enum TextDir {
        ltr, rtl, auto
    }
    public enum DragDrop {
        copy, move, link
    }
    public enum EncType {
        application, multipart, text
    }
    public enum HttpEquiv {
        [Obsolete] content_language,
        content_security_policy,
        [Obsolete] content_type,
        refresh,
        [Obsolete] set_cookie
    }
    public enum TrackKind {
        subtitles, captions, descriptions, chapters, metadata
    }
    public enum HttpMethod {
        post, get
    }
    public enum Preload {
        none, metadata, auto
    }
    public enum Scope {
        row, col, rowgroup, colgroup, auto
    }
    public enum Shape {
        rect, circle, poly, @default, circ, polygon, rectangle
    }
    public enum Target {
        _self, _blank, _parent, _top
    }
    public enum TextWrap {
        hard, soft, off
    }
    [Obsolete("Not Supported In Html5")]
    public enum Valign {
        top, middle, bottom, baseline
    }

    public enum ButtonType {
        button, submit, reset
    }
    public enum InputType {
        button, checkbox, color, date, datetime_local, email, file,
        hidden, image, month, number, password, radio, range, reset,
        search, submit, tel, text, time, url, week
    }
    public enum MenuType {
        list, context, toolbar
    }
}
