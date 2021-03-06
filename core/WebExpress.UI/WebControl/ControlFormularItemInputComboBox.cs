﻿using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputComboBox : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt die Einträge
        /// </summary>
        public List<ControlFormularItemInputComboBoxItem> Items { get; private set; } = new List<ControlFormularItemInputComboBoxItem>();

        ///// <summary>
        ///// Liefert oder setzt das ausgewählte Element
        ///// </summary>
        //public string Selected { get; set; }

        /// <summary>
        /// Liefert oder setzt die OnChange-Attribut
        /// </summary>
        public PropertyOnChange OnChange { get; set; }

        ///// <summary>
        ///// Liefert oder setzt das ausgewählte Element anhand des Wertes
        ///// </summary>
        //public string SelectedValue { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputComboBox(string id = null)
            : base(id)
        {
            Name = id;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die ComboBox-Einträge</param>
        public ControlFormularItemInputComboBox(string id, params string[] items)
            : this(id)
        {
            Items.AddRange(from v in items select new ControlFormularItemInputComboBoxItem() { Text = v });
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="items">Die ComboBox-Einträge</param>
        public ControlFormularItemInputComboBox(string id, params ControlFormularItemInputComboBoxItem[] items)
            : this(id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die ComboBox-Einträge</param>
        public ControlFormularItemInputComboBox(string id, string name, IEnumerable<ControlFormularItemInputComboBoxItem> items)
            : this(id, name)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
            context.Page.AddParam(Name, context.Formular.Scope);

            if (context.Page.HasParam(Name))
            {
                Value = context.Page.GetParamValue(Name);
            }
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {

            var html = new HtmlElementFieldSelect()
            {
                ID = ID,
                Name = Name,
                Class = Css.Concatenate("custom-select", GetClasses()),
                Style = GetStyles(),
                Role = Role,
                Disabled = Disabled,
                OnChange = OnChange?.ToString()
            };

            foreach (var v in Items)
            {
                if (v.SubItems.Count > 0)
                {
                    html.Elements.Add(new HtmlElementFormOptgroup() { Label = v.Text });
                    foreach (var s in v.SubItems)
                    {
                        html.Elements.Add(new HtmlElementFormOption() { Value = s.Value, Text = s.Text, Selected = (s.Value == Value) });
                    }
                }
                else
                {
                    html.Elements.Add(new HtmlElementFormOption() { Value = v.Value, Text = v.Text, Selected = (v.Value == Value) });
                }
            }

            return html;
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
