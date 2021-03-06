﻿using System.Linq;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert einen zum Dokument hinzugefügten Teil.
    /// </summary>
    public class HtmlElementEditIns : HtmlElement, IHtmlElementEdit
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
        /// Liefert oder setzt die URI einer Quelle, die die Änderung ausgelöst hat (z.B. eine Ticketnummer in einem Bugtrack-System).
        /// </summary>
        public string Cite
        {
            get => GetAttribute("cite");
            set => SetAttribute("cite", value);
        }

        /// <summary>
        /// Liefert oder setzt die indiziert das Datum und die Uhrzeit, wann der Text geändert wurde. 
        /// Wenn der Wert nicht als Datum mit optionaler Zeitangabe erkannt werden kann, hat dieses Element keinen Bezug zur Zeit.
        /// </summary>
        public string DateTime
        {
            get => GetAttribute("datetime");
            set => SetAttribute("datetime", value);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementEditIns()
            : base("ins")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="text">Der Inhalt</param>
        public HtmlElementEditIns(string text)
            : this()
        {
            Text = text;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlElementEditIns(params IHtmlNode[] nodes)
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
