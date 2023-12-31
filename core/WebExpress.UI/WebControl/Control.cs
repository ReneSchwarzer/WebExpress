﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public abstract class Control : IControl
    {
        /// <summary>
        /// Die horizontale Anordnung
        /// </summary>
        public virtual TypeHorizontalAlignment HorizontalAlignment
        {
            get => (TypeHorizontalAlignment)GetProperty(TypeHorizontalAlignment.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die Farbe des Textes
        /// </summary>
        public virtual PropertyColorText TextColor
        {
            get => (PropertyColorText)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Die Hintergrundfarbe
        /// </summary>
        public virtual PropertyColorBackground BackgroundColor
        {
            get => (PropertyColorBackground)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Die Rahmenfarbe
        /// </summary>
        public virtual PropertyColorBorder BorderColor
        {
            get => (PropertyColorBorder)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass(), () => value?.ToStyle());
        }

        /// <summary>
        /// Padding
        /// </summary>
        public virtual PropertySpacingPadding Padding
        {
            get => (PropertySpacingPadding)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass());
        }

        /// <summary>
        /// Margin
        /// </summary>
        public virtual PropertySpacingMargin Margin
        {
            get => (PropertySpacingMargin)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass());
        }

        /// <summary>
        /// Rahmen
        /// </summary>
        public virtual PropertyBorder Border
        {
            get => (PropertyBorder)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass());
        }

        /// <summary>
        /// Die Column-Eigenschaft, wenn das Steuerelement in einem Grid befindet
        /// </summary>
        public virtual PropertyGrid GridColumn
        {
            get => (PropertyGrid)GetPropertyObject();
            set => SetProperty(value, () => value?.ToClass());
        }

        /// <summary>
        /// Die Weiten-Eigenschaft, wenn das Steuerelements
        /// </summary>
        public virtual TypeWidth Width
        {
            get => (TypeWidth)GetProperty(TypeWidth.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Die Höhen-Eigenschaft, wenn das Steuerelements
        /// </summary>
        public virtual TypeHeight Height
        {
            get => (TypeHeight)GetProperty(TypeHeight.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Css-Klasse
        /// </summary>
        public List<string> Classes { get; set; } = new List<string>();

        /// <summary>
        /// Liefert oder setzt Eigenschaften, die durch Enums bestimmt werden
        /// </summary>
        protected Dictionary<string, Tuple<object, Func<string>, Func<string>>> Propertys { get; private set; } = new Dictionary<string, Tuple<object, Func<string>, Func<string>>>();

        /// <summary>
        /// Liefert oder setzt die Css-Style
        /// </summary>
        public List<string> Styles { get; set; } = new List<string>();

        /// <summary>
        /// Liefert oder setzt die Rolle
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Liefert oder setzt die OnClick-Attribut
        /// </summary>
        public string OnClick { get; set; }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public abstract IHtmlNode Render(RenderContext context);

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public Control(string id = null)
        {
            ID = id;

            HorizontalAlignment = TypeHorizontalAlignment.Default;
            BackgroundColor = new PropertyColorBackground(TypeColorBackground.Default);
            Padding = new PropertySpacingPadding(PropertySpacing.Space.None);
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None);

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {

        }

        ///// <summary>
        ///// Fügt ein Parameter hinzu. Der Wert wird aus dem Request ermittelt
        ///// </summary>
        ///// <param name="name">Der Name des Parametern</param>
        ///// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        //public void AddParam(string name, ParameterScope scope = ParameterScope.Global)
        //{
        //    Page.AddParam(name.ToLower(), scope);
        //}

        ///// <summary>
        ///// Fügt ein Parameter hinzu.
        ///// </summary>
        ///// <param name="name">Der Name des Parametern</param>
        ///// <param name="value">Der Wert</param>
        ///// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        //public void AddParam(string name, string value, ParameterScope scope = ParameterScope.Global)
        //{
        //    Page.AddParam(name.ToLower(), value, scope);
        //}

        ///// <summary>
        ///// Fügt ein Parameter hinzu. 
        ///// </summary>
        ///// <param name="param">Der Parameter</param>
        ///// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        //public void AddParam(Parameter param)
        //{
        //    Page.AddParam(param);
        //}

        ///// <summary>
        ///// Liefert ein Parameter anhand seines Namens
        ///// </summary>
        ///// <param name="name">Der Name des Parameters</param>
        ///// <returns>Der Wert</returns>
        //public string GetParam(string name)
        //{
        //    return Page.GetParam(name?.ToLower());
        //}

        ///// <summary>
        ///// Liefert ein Parameter anhand seines Namens
        ///// </summary>
        ///// <param name="name">Der Name des Parameters</param>
        ///// <param name="defaultValue">Standardwert</param>
        ///// <returns>Der Wert</returns>
        //public int GetParam(string name, int defaultValue)
        //{
        //    return Page.GetParam(name?.ToLower(), defaultValue);
        //}

        /// <summary>
        /// Liefert eine Property
        /// </summary>
        /// <param name="defaultValue">Der Standardwert Property</param>
        /// <param name="propertyName">Der Name der Property</param>
        /// <returns>Der Wert</returns>
        protected Enum GetProperty(Enum defaultValue, [CallerMemberName] string propertyName = "")
        {
            if (Propertys.ContainsKey(propertyName))
            {
                var item = Propertys[propertyName];

                return (Enum)item.Item1;
            }

            return defaultValue;
        }

        /// <summary>
        /// Liefert eine Property
        /// </summary>
        /// <param name="propertyName">Der Name der Property</param>
        /// <returns>Der Wert</returns>
        protected Enum GetProperty([CallerMemberName] string propertyName = "")
        {
            if (Propertys.ContainsKey(propertyName))
            {
                var item = Propertys[propertyName];

                return (Enum)item.Item1;
            }

            return null;
        }

        /// <summary>
        /// Liefert eine Property
        /// </summary>
        /// <param name="propertyName">Der Name der Property</param>
        /// <returns>Der Wert</returns>
        protected IProperty GetPropertyObject([CallerMemberName] string propertyName = "")
        {
            if (Propertys.ContainsKey(propertyName))
            {
                var item = Propertys[propertyName];

                return (IProperty)item.Item1;
            }

            return null;
        }

        /// <summary>
        /// Liefert ein Propertywert
        /// </summary>
        /// <param name="propertyName">Der Name der Property</param>
        /// <returns>Der Wert</returns>
        protected string GetPropertyValue([CallerMemberName] string propertyName = "")
        {
            if (Propertys.ContainsKey(propertyName))
            {
                var item = Propertys[propertyName];

                return item.Item2();
            }

            return null;
        }

        /// <summary>
        /// Speichert eine Property
        /// </summary>
        /// <param name="value">Der Wert</param>
        /// <param name="callbackClass">Die Rückruffunktion zur Ermittlung der CSS-Klasse</param>
        /// <param name="callbackStyle">Die Rückruffunktion zu Ermittlung des CSS-Styles</param>
        /// <param name="propertyName">Der Name der Property</param>
        protected void SetProperty(Enum value, Func<string> callbackClass, Func<string> callbackStyle = null, [CallerMemberName] string propertyName = "")
        {
            if (!Propertys.ContainsKey(propertyName))
            {
                Propertys.Add(propertyName, new Tuple<object, Func<string>, Func<string>>(value, callbackClass, callbackStyle));
                return;
            }

            Propertys[propertyName] = new Tuple<object, Func<string>, Func<string>>(value, callbackClass, callbackStyle);
        }

        /// <summary>
        /// Speichert eine Property
        /// </summary>
        /// <param name="value">Der Wert</param>
        /// <param name="callbackClass">Die Rückruffunktion zur Ermittlung der CSS-Klasse</param>
        /// <param name="callbackStyle">Die Rückruffunktion zu Ermittlung des CSS-Styles</param>
        /// <param name="propertyName">Der Name der Property</param>
        protected void SetProperty(IProperty value, Func<string> callbackClass, Func<string> callbackStyle = null, [CallerMemberName] string propertyName = "")
        {
            if (!Propertys.ContainsKey(propertyName))
            {
                Propertys.Add(propertyName, new Tuple<object, Func<string>, Func<string>>(value, callbackClass, callbackStyle));
                return;
            }

            Propertys[propertyName] = new Tuple<object, Func<string>, Func<string>>(value, callbackClass, callbackStyle);
        }

        /// <summary>
        /// Speichert eine Property
        /// </summary>
        /// <param name="value">Der Wert</param>
        /// <param name="callbackClass">Die Rückruffunktion zur Ermittlung der CSS-Klasse</param>
        /// <param name="callbackStyle">Die Rückruffunktion zur Ermittlung des CSS-Styles</param>
        /// <param name="propertyName">Der Name der Property</param>
        protected void SetProperty(Func<string> callbackClass, Func<string> callbackStyle = null, [CallerMemberName] string propertyName = "")
        {
            if (!Propertys.ContainsKey(propertyName))
            {
                Propertys.Add(propertyName, new Tuple<object, Func<string>, Func<string>>(null, callbackClass, callbackStyle));
                return;
            }

            Propertys[propertyName] = new Tuple<object, Func<string>, Func<string>>(null, callbackClass, callbackStyle);
        }

        /// <summary>
        /// Liefert alle Css-Klassen
        /// </summary>
        /// <returns>Die CSS-Kalssen</returns>
        protected string GetClasses()
        {
            var list = Propertys.Values.Select(x => x.Item2()).Where(x => !string.IsNullOrEmpty(x)).Distinct();

            return string.Join(" ", Classes.Union(list));
        }

        /// <summary>
        /// Liefert alle Css-Styles
        /// </summary>
        /// <returns>Die CSS-Styles</returns>
        protected string GetStyles()
        {
            var list = Propertys.Values.Where(x => x.Item3 != null).Select(x => x.Item3()).Where(x => !string.IsNullOrEmpty(x)).Distinct();

            return string.Join(" ", Styles.Union(list));
        }

    }
}
