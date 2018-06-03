using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.IO.Compression;

namespace ConsoleApp1
{
    class Program
    {

        public static void main ()
        {
            while (true)
            {
                try
                {
                    Again:
                    Console.ForegroundColor = ConsoleColor.Green;
                    string input = Console.ReadLine();
                    if (input == "cls")
                    {
                        Console.Clear();
                        goto Again;

                    }
                    Console.WriteLine(Automata.start(input));
                }
                catch (AutoException ex)
                {
                    Console.WriteLine(ex.Message + '\t' + ex.Current + '\t' + ex.Demanded);
                }
            }
        }

    }
}
    
