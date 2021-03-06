﻿using System.Collections.Generic;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert eineListe mit Befehlen.
    /// </summary>
    public class HtmlElementInteractiveMenu : HtmlElement, IHtmlElementInteractive
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public new List<IHtmlNode> Elements => base.Elements;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementInteractiveMenu()
            : base("menu")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveMenu(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementInteractiveMenu(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            base.Elements.AddRange(nodes);
        }
    }
}
