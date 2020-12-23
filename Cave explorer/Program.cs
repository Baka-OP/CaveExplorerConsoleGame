using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

using Cave_Explorer.Models;
using Cave_Explorer.Enums;
using Cave_Explorer.Interfaces;
using Cave_Explorer.Graphic_Components;

namespace Cave_Explorer
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            new MainMenu();
        }
    }
}
