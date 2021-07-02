using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace HtmlBuilderWithoutBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var hello = "Hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            WriteLine(sb);

            var words = new[] { "Hello", "World" };
            sb.Clear();
            sb.Append("<ul>");
            sb.AppendFormat(Environment.NewLine);
            foreach (var word in words)
            {
                sb.AppendFormat($"{"",-2}<li>{word}</li>");
                sb.AppendFormat(Environment.NewLine);
            }
            sb.Append("</ul>");
            WriteLine(sb);

        }
    }
}
