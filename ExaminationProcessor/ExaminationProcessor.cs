using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExaminationProcessor
{
    class ExaminationProcessor
    {
        public ExaminationProcessor(string inputPath, string outputPath)
        {
            Exam exam;
            List<string> errors = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(inputPath))
                {
                    ExamStreamReader examReader = new ExamStreamReader(reader);
                    exam = examReader.Read(out errors);
                }

                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    ExamXmlWriter examWriter = new ExamXmlWriter(writer);
                    examWriter.Write(exam, errors);
                }
                Console.WriteLine("Done.");
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
