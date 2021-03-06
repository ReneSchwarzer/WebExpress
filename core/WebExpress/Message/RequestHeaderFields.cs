﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebExpress.Message
{
    /// <summary>
    /// siehe RFC 2616
    /// </summary>
    public class RequestHeaderFields
    {
        /// <summary>
        /// Der Host
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Keep-Alive | close
        /// </summary>
        public string Connection { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Content-Länge
        /// </summary>
        public int ContentLength { get; private set; }

        /// <summary>
        /// Liefert oder setzt den Content-Typ
        /// </summary>
        public string ContentType { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Sprache des Content
        /// </summary>
        public string ContentLanguage { get; private set; }

        /// <summary>
        /// Liefert oder setzt den User-Educationen
        /// </summary>
        public string UserEducation { get; private set; }

        /// <summary>
        /// Liefert oder setzt die erlaubten Endkodierungen
        /// </summary>
        public string AcceptEncoding { get; private set; }

        /// <summary>
        /// Liefert oder setzt die erlaubten Sprachen
        /// </summary>
        public string AcceptLanguage { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Zugangsdaten Name und Passwort
        /// </summary>
        public RequestAuthorization Authorization { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Cookies
        /// </summary>
        public List<Cookie> Cookies { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private RequestHeaderFields()
        {
        }

        /// <summary>
        /// Optionen parsen
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static RequestHeaderFields Parse(List<string> options)
        {
            var obj = new RequestHeaderFields()
            {
                Host = GetOptionsValue(options, "Host"),
                Connection = GetOptionsValue(options, "Connection"),
                ContentType = GetOptionsValue(options, "Content-Type"),
                ContentLanguage = GetOptionsValue(options, "Content-Language"),
                UserEducation = GetOptionsValue(options, "User-Education"),
                AcceptEncoding = GetOptionsValue(options, "Accept-Encoding"),
                AcceptLanguage = GetOptionsValue(options, "Accept-Language")
            };

            var authorization = GetOptionsValue(options, "Authorization");
            obj.Authorization = string.IsNullOrWhiteSpace(authorization) ? null : RequestAuthorization.Parse(authorization);

            try
            {
                obj.ContentLength = Convert.ToInt32(GetOptionsValue(options, "Content-Length"));
            }
            catch
            {
                obj.ContentLength = -1;
            }

            var cookie = GetOptionsValue(options, "Cookie");
            if (!string.IsNullOrWhiteSpace(cookie))
            {
                obj.Cookies = (from c in cookie.Split(';') select new Cookie(c)).ToList();
            }

            return obj;
        }

        /// <summary>
        /// Liefert zu einer Option den Wert
        /// </summary>
        /// <param name="options">Die Optionen</param>
        /// <param name="property">Die Eigenschaft</param>
        /// <returns>Der Wert oder null, wenn nicht vorhanden</returns>
        private static string GetOptionsValue(List<string> options, string property)
        {
            string value = null;

            foreach (var v in options)
            {
                var match = Regex.Match(v, "^" + property + ": (.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                if (match.Success && match.Groups.Count >= 2 && match.Groups[1].Success)
                {
                    return match.Groups[1].Value;
                }
            }

            return value;
        }
    }
}
