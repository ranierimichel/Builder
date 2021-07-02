using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseBuilder
{
    public class CodeElement
    {
        public string Type, Name;
        public List<CodeElement> Elements = new List<CodeElement>();
        public const int indentSize = 1;

        public CodeElement()
        {
        }

        public CodeElement(string name, string type)
        {
            this.Type = type;
            this.Name = name;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            var count = 0;
            if (string.IsNullOrWhiteSpace(Type))
            {
                sb.AppendLine($"public class {Name}");
                sb.AppendLine("{");
                if (Elements.Count == count)
                {
                    sb.AppendLine("}");
                }
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine($"public {Name} {Type};");
            }

            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
                count++;
                if (Elements.Count == count)
                {
                    sb.AppendLine("}");
                }
            }

            return sb.ToString();

        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class CodeBuilder
    {
        private readonly string rootName;
        CodeElement root = new CodeElement();

        public CodeBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public CodeBuilder AddField(string name, string type)
        {
            var e = new CodeElement(type, name);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString(); 
        }

        public void Clear()
        {
            root = new CodeElement();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }
}
