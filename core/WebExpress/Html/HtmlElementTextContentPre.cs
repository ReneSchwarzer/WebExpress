﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert den Inhalt dieses Elements als vorformatiert und das dieses Format erhalten bleiben soll.
    /// </summary>
    public class HtmlElementTextContentPre : HtmlElement, IHtmlElementTextContent
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTextContentPre()
            : base("pre")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentPre(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTextContentPre(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
