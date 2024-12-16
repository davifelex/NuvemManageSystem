using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuvemManageSystem.SupportFunctions
{
    internal class InputFunctions
    {
        string? Response = null;
        public string? InputString(bool _null, string text, string title)
        {
            Console.WriteLine(text);
            Response = Console.ReadLine();
            if (_null == false && Response == null || Response == "")
            {
                InputNotNull(text, title);
            }

            return Response;
        }

        public int InputInt(string text, string title)
        {
            int result;
            Console.WriteLine(text);
            Response = Console.ReadLine();
            while (!int.TryParse(Response, out result))
            {
                Console.WriteLine($"Digite um {title} válido: ");
                Response = Console.ReadLine();
            }
            return result;
        }

        public float InputFloat(string text, string title)
        {
            float result;
            Console.WriteLine(text);
            Response = Console.ReadLine();
            while (!float.TryParse(Response, out result))
            {
                Console.WriteLine($"Digite um {title} válido: ");
                Response = Console.ReadLine();
            }
            return result;
        }

        public double InputDouble(string text, string title)
        {
            double result;
            Console.WriteLine(text);
            Response = Console.ReadLine();
            while (!Double.TryParse(Response, out result))
            {
                Console.WriteLine($"Digite um {title} válido: ");
                Response = Console.ReadLine();
            }
            return result;
        }

        private void InputNotNull(string text, string title)
        {
            while (Response == null || Response == "")
            {
                Console.WriteLine($"Digite um {title} válido: ");
                Response = Console.ReadLine();
            }
        }
    }
}
