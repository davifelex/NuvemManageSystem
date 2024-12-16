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
            Console.WriteLine(text + _null);
            Response = Console.ReadLine();
            if (_null == false && Response == null || Response == "")
            {
                InputNotNull(text, title);
            }

            return Response;
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
