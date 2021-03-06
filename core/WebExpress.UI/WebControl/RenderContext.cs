﻿using System.Globalization;
using WebExpress.Internationalization;
using WebExpress.Uri;
using WebExpress.WebResource;

namespace WebExpress.UI.WebControl
{
    public class RenderContext : II18N
    {
        /// <summary>
        /// Die Seite, indem das Steuerelement gerendert wird
        /// </summary>
        public IPage Page { get; private set; }

        /// <summary>
        /// Die Uir der Seite
        /// </summary>
        public IUri Uri => Page?.Uri;

        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18N_PluginID => Page.Context.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        public RenderContext(IPage page)
        {
            Page = page;
            Culture = (Page as Resource).Culture;
        }
    }
}
