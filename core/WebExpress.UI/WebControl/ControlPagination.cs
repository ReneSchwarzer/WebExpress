﻿using System;
using System.Collections.Generic;
using WebExpress.Html;
using WebExpress.Message;

namespace WebExpress.UI.WebControl
{
    public class ControlPagination : Control
    {
        /// <summary>
        /// Liefert oder setzt die Anzahl der Seiten
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Liefert oder setzt die Seitengröße
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Liefert oder setzt die aktuelle Seite
        /// </summary>
        public int PageOffset { get; set; }

        /// <summary>
        /// Liefert oder setzt die maximale Anzahl der Seitenschaltflächen
        /// </summary>
        public int MaxDisplayCount { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypeSizePagination Size
        {
            get => (TypeSizePagination)GetProperty(TypeSizePagination.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPagination(string id = null)
            : base(id)
        {
            MaxDisplayCount = 5;

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            //AddParam("count");
            //AddParam("size", ParameterScope.Session);
            //AddParam("offset", ParameterScope.Local);

            ////Count = GetParam("count", 0);
            //Size = GetParam("size", 50);
            //Offset = GetParam("offset", 0);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentUl()
            {
                Class = Css.Concatenate("pagination", Css.Remove(GetClasses(), BackgroundColor?.ToClass(), BorderColor?.ToClass())),
                Style = Style.Remove(GetStyles(), BackgroundColor.ToStyle()),
                Role = Role
            };

            if (PageOffset >= PageCount)
            {
                PageOffset = PageCount - 1;
            }

            if (PageOffset < 0)
            {
                PageOffset = 0;
            }

            if (PageOffset > 0 && PageCount > 1)
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(new Parameter("offset", PageOffset - 1) { Scope = ParameterScope.Local }),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-left", "border-0" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item"
                    }
                );
            }
            else
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-left", "border-0" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item disabled"
                    }
                );
            }

            var buf = new List<int>(MaxDisplayCount);

            var j = 0;
            var k = 0;

            buf.Add(PageOffset);
            while (buf.Count < Math.Min(PageCount, MaxDisplayCount))
            {
                if (PageOffset + j + 1 < PageCount)
                {
                    j += 1;
                    buf.Add(PageOffset + j);
                }

                if (PageOffset - k - 1 >= 0)
                {
                    k += 1;
                    buf.Add(PageOffset - k);
                }
            }

            buf.Sort();

            foreach (var v in buf)
            {
                if (v == PageOffset)
                {
                    html.Elements.Add
                    (
                        new HtmlElementTextContentLi
                        (
                            new ControlLink(null, (v + 1).ToString())
                            {
                                BackgroundColor = BackgroundColor,
                                Params = Parameter.Create(new Parameter("offset", v) { Scope = ParameterScope.Local }),
                                Classes = new List<string>() { Css.Concatenate("page-link border-0") },
                                Styles = new List<string>() { Style.Concatenate("", BackgroundColor.ToStyle()) }
                            }.Render(context)
                        )
                        {
                            Class = "page-item active"
                        }
                    );
                }
                else
                {
                    html.Elements.Add
                    (
                        new HtmlElementTextContentLi
                        (
                            new ControlLink(null, (v + 1).ToString())
                            {
                                Params = Parameter.Create(new Parameter("offset", v) { Scope = ParameterScope.Local }),
                                Classes = new List<string>(new[] { "page-link border-0" })
                            }.Render(context)
                        )
                        {
                            Class = "page-item"
                        }
                    );
                }
            }

            if (PageOffset < PageCount - 1)
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(new Parameter("offset", PageOffset + 1) { Scope = ParameterScope.Local }),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-right", "border-0" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item"
                    }
                );
            }
            else
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-right", "border-0" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item disabled"
                    }
                );
            }

            return html;
        }
    }
}
