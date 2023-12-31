﻿using WebExpress.Uri;
using Xunit;

namespace WebExpress.Test.Uri
{
    public class UnitTestUriAbsolute
    {
        [Fact]
        public void New_0()
        {
            var uri = new UriAbsolute("http://user@example.com:8080/abc#a?b=1&c=2");

            Assert.True
            (
                uri.Scheme == UriScheme.Http,
                "Fehler in der Funktion New_0 der UriRelative"
            );
        }

        [Fact]
        public void New_1()
        {
            var uri = new UriAbsolute("http://vila/assets/img/vila.svg");

            Assert.True
            (
                uri.Scheme == UriScheme.Http,
                "Fehler in der Funktion New_1 der UriRelative"
            );
        }

        [Fact]
        public void New_2()
        {
            var uri = new UriAbsolute("http://localhost");

            Assert.True
            (
                uri.Scheme == UriScheme.Http &&
                uri.Authority.Host == "localhost",
                "Fehler in der Funktion New_2 der UriRelative"
            );
        }
    }
}
