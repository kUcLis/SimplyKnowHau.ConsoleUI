using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI
{
    internal class LogoAndHelpers
    {
        const ConsoleColor BG = ConsoleColor.Black;
        const ConsoleColor BG_ACTIVE = ConsoleColor.DarkYellow;
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;

        static string simplyLogo = @"
                                                             
,---.o          |         |   /               |   |          
`---..,-.-.,---.|    ,   .|__/ ,---.,---.. . .|---|,---..   .
    ||| | ||   ||    |   ||  \ |   ||   || | ||   |,---||   |
`---'`` ' '|---'`---'`---|`   ``   '`---'`-'-'`   '`---^`---'
           |         `---'                                   
";

        public static void SetCursorAndMsg(int cursor, string? msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition((Console.WindowWidth - cursor) / 2, Console.CursorTop);
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        public static string? SetCursorAndRead(int cursor)
        {

            Console.SetCursorPosition((Console.WindowWidth - cursor) / 2, Console.CursorTop);
            string input = Console.ReadLine();
            return input;
        }

        public static void DisplayLogo()
        {


            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = FG;
            Console.WriteLine(simplyLogo);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Made entirely by kUcLis");
            Console.ForegroundColor = FG;
            Console.WriteLine();

        }

    }
}

