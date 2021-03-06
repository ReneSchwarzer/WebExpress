﻿using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für eine Auswahloption innerhalb eines <select>-Elements, oder einen Vorschlag innerhalb eines <datalist>-Elements.
    /// <select name="top5" size="5">
    ///  <option>Michael Jackson</option>
    ///  <option selected>Tom Waits</option>
    /// </select>
    /// </summary>
    public class HtmlElementFormOption : HtmlElement, IHtmlFormularItem
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text
        {
            get => string.Join("", Elements.Where(x => x is HtmlText).Select(x => (x as HtmlText).Value));
            set { Elements.Clear(); Elements.Add(new HtmlText(value)); }
        }

        /// <summary>
        /// Liefert oder setzt einen Wert
        /// </summary>
        public string Value
        {
            get => GetAttribute("value");
            set => SetAttribute("value", value);
        }

        /// <summary>
        /// Liefert oder setzt ob das Felf ausgewählt ist
        /// </summary>
        public bool Selected
        {
            get => HasAttribute("selected");
            set { if (value) { SetAttribute("selected"); } else { RemoveAttribute("selected"); } }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementFormOption()
            : base("option")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementFormOption(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            base.ToString(builder, deep);
        }
    }
}
