﻿namespace WebExpress.Html
{
    public enum TypeTarget
    {
        None,
        Blank,
        Self,
        Parent,
        Top,
        Framename
    }

    public static class TypeTargetExtensions
    {
        /// <summary>
        /// Umwandlung in einen Klartext
        /// </summary>
        /// <param name="target">Das Aufrufsziel</param>
        /// <returns>Der Klartext des Targets</returns>
        public static string ToStringValue(this TypeTarget target)
        {
            switch (target)
            {
                case TypeTarget.Blank:
                    return "_blank";
                case TypeTarget.Self:
                    return "_self";
                case TypeTarget.Parent:
                    return "_parent";
                case TypeTarget.Top:
                    return "_top";
                case TypeTarget.Framename:
                    return "_framename";
            }

            return string.Empty;
        }
    }
}
