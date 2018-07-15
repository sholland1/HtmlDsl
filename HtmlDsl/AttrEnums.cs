using System;

namespace HtmlDsl {
    public enum Align {
        left, right, center, justify
    }
    public enum Capitalize {
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
    public enum Valign {
        top, middle, bottom, baseline
    }
}
