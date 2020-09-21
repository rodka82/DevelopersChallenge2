using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperChallenge.Api.Helpers
{
    [HtmlTargetElement("angular-app")]
    public class AngularAppTagHelper : TagHelper
    {

        public AngularAppTagHelper()
        {

        }

        public string App { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            using (var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\NiboApp\dist" + App + @"\index.html"))
            {
                string htmlRaw = sr.ReadToEnd();

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlRaw);

                var body = htmlDoc.DocumentNode.ChildNodes.FindFirst("body").ChildNodes.Where(x => x.Name == "script");
                StringBuilder sb = new StringBuilder("");

                foreach (var item in body)
                {
                    sb.AppendLine($"<script src='/NiboApp/dist/{App}/{item.Attributes["src"].Value}'></script>");
                }

                output.Content.AppendHtml(new HtmlString(sb.ToString()));
            }

        }
    }
}
