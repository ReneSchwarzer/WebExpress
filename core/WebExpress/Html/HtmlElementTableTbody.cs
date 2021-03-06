﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für die Spalten, die die eigentlichen Daten einer Tabelle enthalten.
    /// </summary>
    public class HtmlElementTableTbody : HtmlElement, IHtmlElementTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTableTbody()
            : base("tbody")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTbody(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTbody(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
