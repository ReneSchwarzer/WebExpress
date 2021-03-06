﻿using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Internationalization;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Gruppierung von Steuerelementen
    /// </summary>
    public class ControlFormularItemGroupColumn : ControlFormularItemGroup
    {
        /// <summary>
        /// Liefert die prozentuale Verteilung der Spalten
        /// </summary>
        public ICollection<int> Distribution { get; set; } = new List<int>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemGroupColumn(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        ///<param name="items">Das Formularsteuerelemente</param> 
        public ControlFormularItemGroupColumn(string id, params ControlFormularItem[] items)
            : base(id, items)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        ///<param name="items">Das Formularsteuerelemente</param> 
        public ControlFormularItemGroupColumn(params ControlFormularItem[] items)
            : base(null, items)
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            var grpupContex = new RenderContextFormularGroup(context, this);

            foreach (var item in Items)
            {
                item.Initialize(grpupContex);
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            var renderContext = new RenderContextFormularGroup(context, this);

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = Css.Concatenate("form-group-horizontal", GetClasses()),
                Style = GetStyles(),
            };

            var body = new HtmlElementTextContentDiv() {};
            
            foreach (var item in Items)
            {
                var input = item as ControlFormularItemInput;
                var row = new HtmlElementTextContentDiv() {};

                if (input != null)
                {
                    var icon = new ControlIcon() { Icon = input?.Icon };
                    var label = new ControlFormularItemLabel(!string.IsNullOrEmpty(item.ID) ? item.ID + "_label" : string.Empty);
                    var help = new ControlFormularItemHelpText(!string.IsNullOrEmpty(item.ID) ? item.ID + "_help" : string.Empty);

                    label.Initialize(renderContext);
                    help.Initialize(renderContext);

                    label.Text = context.I18N(input?.Label);
                    label.FormularItem = item;
                    label.Classes.Add("mr-2");
                    help.Text = context.I18N(input?.Help);
                    help.Classes.Add("ml-2");

                    if (icon.Icon != null)
                    {
                        icon.Classes.Add("mr-2 pt-1");

                        row.Elements.Add(new HtmlElementTextContentDiv(icon.Render(renderContext), label.Render(renderContext)) { });
                    }
                    else
                    {
                        row.Elements.Add(new HtmlElementTextContentDiv(label.Render(renderContext)));
                    }

                    row.Elements.Add(new HtmlElementTextContentDiv(item.Render(renderContext)) { });

                    if (input != null)
                    {
                        row.Elements.Add(new HtmlElementTextContentDiv(help.Render(renderContext)));
                    }
                }
                else
                {
                    row.Elements.Add(new HtmlElementTextContentDiv());
                    row.Elements.Add(item.Render(context));
                    row.Elements.Add(new HtmlElementTextContentDiv());
                }

                body.Elements.Add(row);
            }

            html.Elements.Add(body);

            return html;
        }
    }
}
