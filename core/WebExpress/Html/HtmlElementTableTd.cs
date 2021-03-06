﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Kennzeichnet eine einzelne Tabellenzelle.
    /// </summary>
    public class HtmlElementTableTd : HtmlElement, IHtmlElementTable
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTableTd()
            : base("td")
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="node">Der Inhalt</param>
        public HtmlElementTableTd(IHtmlNode node)
            : this()
        {
            Elements.Add(node);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTd(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementTableTd(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }
    }
}
