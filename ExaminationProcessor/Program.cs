using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExaminationProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: ExaminationProcessor.exe <path\\to\\input\\file.txt> <path\\to\\output\\file.xml>");
            }
            else
            {
                new ExaminationProcessor(args[0], args[1]);
            }
        }
    }
}
