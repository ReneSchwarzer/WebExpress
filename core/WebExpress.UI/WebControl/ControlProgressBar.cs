﻿using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlProgressBar : Control
    {
        /// <summary>
        /// Liefert oder setzt das Format des Fortschrittbalkens
        /// </summary>
        public TypeFormatProgress Format { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypeSizeProgress Size
        {
            get => (TypeSizeProgress)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass(), () => value.ToStyle());
        }

        /// <summary>
        /// Liefert oder setzt die Farbe des Balkens
        /// </summary>
        public PropertyColorProgress Color { get; set; }

        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public new PropertyColorText TextColor { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Liefert oder setzt den Minimumwert
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Liefert oder setzt dem Maximumwert
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlProgressBar(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Wert</param>
        public ControlProgressBar(string id, int value)
            : this(id)
        {
            Value = value;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="value">Der Wert</param>
        public ControlProgressBar(string id, int value, int min = 0, int max = 100)
            : this(id)
        {
            Value = value;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Min = 0;
            Max = 100;
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Default);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            if (Format == TypeFormatProgress.Default)
            {
                return new HtmlElementFormProgress(Value + "%")
                {
                    ID = ID,
                    Class = GetClasses(),
                    Style = GetStyles(),
                    Role = Role,
                    Min = Min.ToString(),
                    Max = Max.ToString(),
                    Value = Value.ToString()
                };
            }

            var bar = new HtmlElementTextContentDiv(new HtmlText(Text))
            {
                Role = "progressbar",
                Class = Css.Concatenate
                (
                    "progress-bar",
                    Color?.ToClass(),
                    TextColor?.ToClass(),
                    Format.ToClass()
                ),
                Style = Css.Concatenate
                (
                    "width: " + Value + "%;",
                    Color?.ToStyle(),
                    TextColor?.ToStyle()
                )
            };
            bar.AddUserAttribute("aria-valuenow", Value.ToString());
            bar.AddUserAttribute("aria-valuemin", Min.ToString());
            bar.AddUserAttribute("aria-valuemax", Max.ToString());

            var html = new HtmlElementTextContentDiv(bar)
            {
                ID = ID,
                Role = Role,
                Class = Css.Concatenate
                (
                    "progress",
                    GetClasses()
                ),
                Style = GetStyles()
            };

            return html;

        }
    }
}
