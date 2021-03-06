﻿using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlTable : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout der Spaltenüberschrift
        /// </summary>
        public TypesLayoutTableRow ColumnLayout { get; set; }

        /// <summary>
        /// Liefert oder setzt die Spalten
        /// </summary>
        public List<ControlTableColumn> Columns { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Zeilen
        /// </summary>
        public List<ControlTableRow> Rows { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Responsive-Eigenschaft
        /// </summary>
        public bool Responsive { get; set; }

        /// <summary>
        /// Liefert oder setzt die Striped-Eigenschaft
        /// </summary>
        public bool Striped { get; set; }

        /// <summary>
        /// Liefert oder setzt das die Tabelle gedreht wird
        /// </summary>
        public bool Reflow { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTable(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Striped = true;
            Columns = new List<ControlTableColumn>();
            Rows = new List<ControlTableRow>();
        }

        /// <summary>
        /// Fügt eine Spalte hinzu
        /// </summary>
        /// <param name="name">Name der Spalte</param>
        /// <returns></returns>
        public virtual void AddColumn(string name)
        {
            Columns.Add(new ControlTableColumn(null)
            {
                Text = name
            });
        }

        /// <summary>
        /// Fügt eine Spalte hinzu
        /// </summary>
        /// <param name="name">Name der Spalte</param>
        /// <param name="icon">Das Icon der Spalte</param>
        /// <returns></returns>
        public virtual void AddColumn(string name, PropertyIcon icon)
        {
            Columns.Add(new ControlTableColumn(null)
            {
                Text = name,
                Icon = icon
            });
        }

        /// <summary>
        /// Fügt eine Spalte hinzu
        /// </summary>
        /// <param name="name">Name der Spalte</param>
        /// <param name="icon">Das Icon der Spalte</param>
        /// <param name="layout">Das Layout der Spalte</param>
        /// <returns></returns>
        public virtual void AddColumn(string name, PropertyIcon icon, TypesLayoutTableRow layout)
        {
            Columns.Add(new ControlTableColumn(null)
            {
                Text = name,
                Icon = icon,
                Layout = layout
            });
        }

        /// <summary>
        /// Fügt eine Zeile hinzu
        /// </summary>
        /// <param name="cells">Die Zellen der Zeile</param>
        /// <param name="cssClass">Die Css-Klasse</param>
        public void AddRow(Control[] cells, string cssClass = null)
        {
            var r = new ControlTableRow(null) { Classes = new List<string>(new[] { cssClass }) };
            r.Cells.AddRange(cells);

            Rows.Add(r);
        }

        /// <summary>
        /// Fügt eine Zeile hinzu
        /// </summary>
        /// <param name="cells">Die Zellen der Zeile</param>
        /// <param name="layout">Das Layout</param>
        /// <param name="cssClass">Die Css-Klasse</param>
        public void AddRow(Control[] cells, TypesLayoutTableRow layout, string cssClass = null)
        {
            var r = new ControlTableRow(null) { Classes = new List<string>(new[] { cssClass }), Layout = layout };
            r.Cells.AddRange(cells);

            Rows.Add(r);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Columns.ForEach(x => x.Layout = ColumnLayout);

            Classes.Add("table");

            if (Striped)
            {
                Classes.Add("table-striped");
            }

            if (Responsive)
            {
                Classes.Add("table-responsive");
            }

            if (Reflow)
            {
                Classes.Add("table-reflow");
            }

            var html = new HtmlElementTableTable()
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            html.Columns = new HtmlElementTableTr(Columns.Select(x => x.Render(context)));
            html.Rows.AddRange(from x in Rows select x.Render(context) as HtmlElementTableTr);

            return html;
        }
    }
}
