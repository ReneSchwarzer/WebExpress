﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// 
    /// </summary>
    public class HtmlElementEmbeddedPicture : HtmlElement, IHtmlElementEmbedded
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementEmbeddedPicture()
            : base("picture")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedPicture(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEmbeddedPicture(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
