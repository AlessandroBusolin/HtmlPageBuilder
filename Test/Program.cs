﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlPageBuilder;
using WatsonWebserver;

namespace Test
{
    class Program
    {
        static Server _Server = null;
        static HtmlPage _Page = null;
        static string _PageString = null;

        static void Main(string[] args)
        {
            _Page = new HtmlPage();
            _Page.Head.Title = "My Page";
            _Page.Head.MetaKeywords = "my,page";
            _Page.Head.Style = "h1 { font-family: 'arial' } p { font-family: 'arial' } ul { font-family: 'arial' }";
            _Page.Body.Content += _Page.Body.H1Text("My Page");
            _Page.Body.Content += _Page.Body.Paragraph("This is some sample text.");
            _Page.Body.Content += _Page.Body.UnorderedList(new List<string> { "foo", "bar", "baz" });
            _PageString = _Page.ToString();

            _Server = new Server("localhost", 9000);
            _Server.Routes.Default = DefaultRoute;
            _Server.Start();

            Console.WriteLine("Listening on http://localhost:9000, press ENTER to exit");
            Console.ReadLine();
        }

        static async Task DefaultRoute(HttpContext ctx)
        {
            ctx.Response.ContentType = "text/html";
            ctx.Response.ContentLength = _PageString.Length;
            await ctx.Response.Send(_PageString);
        }
    }
}
