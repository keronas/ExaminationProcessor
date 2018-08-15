using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExaminationProcessor
{
    class ExamStreamReader
    {
        private StreamReader reader;
        private List<string> errors;

        public ExamStreamReader(StreamReader input)
        {
            reader = input;
        }

        public Exam Read(out List<string> errors)
        {
            Exam exam = new Exam();

            this.errors = new List<string>();

            exam.Name = reader.ReadLine();
            DeWhiteSpace();

            while (!reader.EndOfStream)
            {
                exam.Groups.Add(ReadGroup());
            }

            errors = this.errors;
            return exam;
        }

        private Group ReadGroup()
        {
            Group group = new Group
            {
                Name = reader.ReadLine()
            };

            Examinee examinee;
            while (true)
            {
                try
                {
                    examinee = ReadExaminee();
                    if (examinee == null)
                    {
                        break;
                    }
                    group.Examinees.Add(examinee);
                }
                catch (InvalidDataException e)
                {
                    errors.Add(e.Message);
                }
            }

            DeWhiteSpace();
            return group;
        }

        private Examinee ReadExaminee()
        {
            Examinee examinee;
            string line = reader.ReadLine();
            string[] fields;

            if (line != null && line.Length != 0)
            {
                examinee = new Examinee();
                fields = line.Split(';');
                if (fields.Length != 4)
                {
                    throw new InvalidDataException("Examinee has invalid number of fields: " + line);
                }
                examinee.Name = fields[0];
                try
                {
                    examinee.Results[(int)Exam.Subject.Math] = ReadSubject(fields[1], "Math", line);
                    examinee.Results[(int)Exam.Subject.Physics] = ReadSubject(fields[2], "Physics", line);
                    examinee.Results[(int)Exam.Subject.English] = ReadSubject(fields[3], "English", line);
                }
                catch (InvalidDataException e)
                {
                    errors.Add(e.Message);
                }
            }
            else
            {
                examinee = null;
            }
            
            return examinee;
        }

        private byte ReadSubject(string field, string nameCheck, string line)
        {
            string[] parts = field.Split('=');
            byte result;
            if (parts.Length != 2)
            {
                throw new InvalidDataException("Examinee has invalid field format: " + line);
            }
            if (parts[0] != nameCheck)
            {
                throw new InvalidDataException("Examinee has invalid field names: " + line);
            }
            try
            {
                result = byte.Parse(parts[1]);
                if (result > 100)
                {
                    throw new InvalidDataException("Examinees results are out of range: " + line);
                }
            }
            catch (FormatException)
            {
                throw new InvalidDataException("Examinee has invalid field format: " + line);
            }
            catch (OverflowException)
            {
                throw new InvalidDataException("Examinees results are out of range: " + line);
            }
            return result;
        }

        private void DeWhiteSpace()
        {
            int next = reader.Peek();
            while (next == '\r' || next == '\n')
            {
                reader.ReadLine();
                next = reader.Peek();
            }
        }
    }
}
