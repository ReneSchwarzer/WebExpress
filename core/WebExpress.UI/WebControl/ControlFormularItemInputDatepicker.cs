﻿using System;
using System.Linq;
using WebExpress.Html;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputDatepicker : ControlFormularItemInput
    {
        /// <summary>
        /// Das Steuerelement wird automatisch initialisiert
        /// </summary>
        public bool AutoInitialize { get; set; }

        /// <summary>
        /// Liefert oder setzt die Beschreibung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Liefert oder setzt die minimale Länge
        /// </summary>
        public string MinLength { get; set; }

        /// <summary>
        /// Liefert oder setzt die maximale Länge
        /// </summary>
        public string MaxLength { get; set; }

        /// <summary>
        /// Liefert oder setzt ob Eingaben erzwungen werden
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Liefert oder setzt ein Suchmuster, welches den Inhalt prüft
        /// </summary>
        public string Pattern { get; set; }

        ///// <summary>
        ///// Liefert den Initialisierungscode (JQuerry)
        ///// </summary>
        //public string InitializeCode => "$('#" + ID + " input').datepicker({ startDate: -3 });";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputDatepicker(string id = null)
            : base(!string.IsNullOrWhiteSpace(id) ? id : "datepicker")
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            AutoInitialize = true;

            if (context.Page.HasParam(Name))
            {
                Value = context.Page.GetParamValue(Name);
            }

            var module = ModuleManager.GetModule("webexpress");
            if (module != null)
            {
                context.Page.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/bootstrap-datepicker.min.js")));
                context.Page.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/locales_datepicker/bootstrap-datepicker." + context.Culture.TwoLetterISOLanguageName.ToLower() + ".min.js")));
                context.Page.CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/bootstrap-datepicker3.min.css")));
            }

            context.Page.AddScript(ID, @"$('#" + ID + @"').datepicker({format: ""dd.mm.yyyy"", todayBtn: true, language: ""de"", zIndexOffset: 999});");
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            

            //if (Disabled)
            //{
            //    Classes.Add("disabled");
            //}

            //if (AutoInitialize)
            //{
            //    context.Page.AddScript(ID, InitializeCode);
            //    AutoInitialize = false;
            //}

            var input = new HtmlElementFieldInput()
            {
                ID = ID,
                Name = Name,
                Type = "text",
                Class = "form-control",
                Value = Value
            };
            
            //var span = new HtmlElementTextSemanticsSpan()
            //{
            //    Class = TypeIcon.Calendar.ToClass()
            //};

            //var div = new HtmlElementTextContentDiv(span)
            //{
            //    Class = "input-group-text"
            //};

            //var html = new HtmlElementTextContentDiv(input, div)
            //{
            //    ID = ID,
            //    Class = "input-group",
            //    //DataProvide = "datepicker"
            //};

            return input;
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            //if (!string.IsNullOrWhiteSpace(Value))
            //{
            //    try
            //    {
            //        var date = Convert.ToDateTime(Value);
            //    }
            //    catch
            //    {
            //        ValidationResults.Add(new ValidationResult() { Type = TypesInputValidity.Error, Text = "Der angegebene Wert kann nicht in ein Datum konvertiert werden!" });
            //    }
            //}

            base.Validate();
        }
    }
}
