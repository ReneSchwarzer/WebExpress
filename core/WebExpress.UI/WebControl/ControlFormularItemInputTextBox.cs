﻿using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.Module;
using WebExpress.UI.Script;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputTextBox : ControlFormularItemInput
    {
        /// <summary>
        /// Das Steuerelement wird automatisch initialisiert
        /// </summary>
        public bool AutoInitialize { get; set; }

        /// <summary>
        /// Bestimmt, ob es sich um ein mehrzeileige TextBox handelt
        /// </summary>
        public TypesEditTextFormat Format { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Platzhaltertext
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// Liefert oder setzt die minimale Länge
        /// </summary>
        public int? MinLength { get; set; }

        /// <summary>
        /// Liefert oder setzt die maximale Länge
        /// </summary>
        public int? MaxLength { get; set; }

        /// <summary>
        /// Liefert oder setzt ob Eingaben erzwungen werden
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Suchmuster, welches den Inhalt prüft
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Liefert oder setzt die Höhe des Textfeldes (bei Multiline und WYSIWYG)
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Liefert den Initialisierungscode (JQuerry)
        /// </summary>
        public string InitializeCode => new JQuerry(!string.IsNullOrWhiteSpace(ID) ? ID : "summernote").ToString() + ".summernote({ tabsize: 2, height: '" + Rows + "rem', lang: 'de-DE' });";

        /// <summary>
        /// Liefert den Zerstörungscode (JQuerry)
        /// </summary>
        public string DestroyCode => new JQuerry(!string.IsNullOrWhiteSpace(ID) ? ID : "summernote").ToString() + ".summernote('destroy');";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputTextBox(string id = null)
            : base(id)
        {
            Name = ID;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="name">Der Name der TextBox</param>
        public ControlFormularItemInputTextBox(string id, string name)
            : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            Rows = 8;
            AutoInitialize = true;

            if (context.Page.HasParam(Name))
            {
                Value = context?.Page.GetParamValue(Name);
            }

            if (Format == TypesEditTextFormat.Wysiwyg)
            {
                var module = ModuleManager.GetModule("webexpress");
                if (module != null)
                {
                    context.Page.CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/summernote-bs4.min.css")));
                    context.Page.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/summernote-bs4.min.js")));
                }
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            Classes.Add("form-control");

            if (Disabled)
            {
                Classes.Add("disabled");
            }

            switch (ValidationResult)
            {
                case TypesInputValidity.Success:
                    Classes.Add("input-success");
                    break;
                case TypesInputValidity.Warning:
                    Classes.Add("input-warning");
                    break;
                case TypesInputValidity.Error:
                    Classes.Add("input-error");
                    break;
            }

            if (AutoInitialize && Format == TypesEditTextFormat.Wysiwyg && !string.IsNullOrWhiteSpace(ID))
            {
                context.Page.AddScript(ID, InitializeCode);
                AutoInitialize = false;
            }

            return Format switch
            {
                TypesEditTextFormat.Multiline => new HtmlElementFormTextarea()
                {
                    ID = ID,
                    Value = Value,
                    Name = Name,
                    Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Role = Role,
                    Placeholder = Placeholder,
                    Rows = Rows.ToString()
                },
                TypesEditTextFormat.Wysiwyg => new HtmlElementFormTextarea()
                {
                    ID = ID,
                    Value = Value,
                    Name = Name,
                    Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Role = Role,
                    Placeholder = Placeholder,
                    Rows = Rows.ToString()
                },
                _ => new HtmlElementFieldInput()
                {
                    ID = ID,
                    Value = Value,
                    Name = Name,
                    MinLength = MinLength?.ToString(),
                    MaxLength = MaxLength?.ToString(),
                    Required = Required,
                    Pattern = Pattern,
                    Type = "text",
                    Disabled = Disabled,
                    Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                    Role = Role,
                    Placeholder = Placeholder
                },
            };
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            if (Disabled)
            {
                return;
            }

            if (Required && string.IsNullOrWhiteSpace(base.Value))
            {
                ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Das Textfeld darf nicht leer sein!" });

                return;
            }

            if (!string.IsNullOrWhiteSpace(MinLength?.ToString()) && Convert.ToInt32(MinLength) > base.Value.Length)
            {
                ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der Text entsprcht nicht der minimalen Länge von " + MinLength + "!" });
            }

            if (!string.IsNullOrWhiteSpace(MaxLength?.ToString()) && Convert.ToInt32(MaxLength) < base.Value.Length)
            {
                ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der Text ist größer als die maximalen Länge von " + MaxLength + "!" });
            }

            base.Validate();
        }
    }
}
