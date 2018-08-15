using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ExaminationProcessor
{
    class ExamXmlWriter
    {
        private readonly StreamWriter SWriter;

        public ExamXmlWriter(StreamWriter output)
        {
            SWriter = output;
        }

        public void Write(Exam exam, List<string> errors = null)
        {
            using (XmlWriter writer = XmlWriter.Create(SWriter))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Report");
                writer.WriteStartElement("Exam");
                writer.WriteElementString("Name", exam.Name);

                writer.WriteStartElement("Averages");
                writer.WriteElementString("Math",    exam.SubjectAverage(Exam.Subject.Math).ToString());
                writer.WriteElementString("Physics", exam.SubjectAverage(Exam.Subject.Physics).ToString());
                writer.WriteElementString("English", exam.SubjectAverage(Exam.Subject.English).ToString());
                writer.WriteEndElement(); // Averages

                writer.WriteStartElement("Medians");
                writer.WriteElementString("Math",    exam.SubjectMedian(Exam.Subject.Math).ToString());
                writer.WriteElementString("Physics", exam.SubjectMedian(Exam.Subject.Physics).ToString());
                writer.WriteElementString("English", exam.SubjectMedian(Exam.Subject.English).ToString());
                writer.WriteEndElement(); // Medians

                writer.WriteStartElement("Modes");
                writer.WriteElementString("Math",    exam.SubjectMode(Exam.Subject.Math).ToString());
                writer.WriteElementString("Physics", exam.SubjectMode(Exam.Subject.Physics).ToString());
                writer.WriteElementString("English", exam.SubjectMode(Exam.Subject.English).ToString());
                writer.WriteEndElement(); // Modes

                writer.WriteStartElement("Groups");
                foreach (Group group in exam.Groups)
                {
                    writer.WriteStartElement("Group");
                    writer.WriteElementString("Name", group.Name);

                    writer.WriteStartElement("Averages");
                    writer.WriteElementString("Math",    group.SubjectAverage(Exam.Subject.Math).ToString());
                    writer.WriteElementString("Physics", group.SubjectAverage(Exam.Subject.Physics).ToString());
                    writer.WriteElementString("English", group.SubjectAverage(Exam.Subject.English).ToString());
                    writer.WriteEndElement(); // Averages

                    writer.WriteStartElement("Medians");
                    writer.WriteElementString("Math",    group.SubjectMedian(Exam.Subject.Math).ToString());
                    writer.WriteElementString("Physics", group.SubjectMedian(Exam.Subject.Physics).ToString());
                    writer.WriteElementString("English", group.SubjectMedian(Exam.Subject.English).ToString());
                    writer.WriteEndElement(); // Medians

                    writer.WriteStartElement("Modes");
                    writer.WriteElementString("Math",    group.SubjectMode(Exam.Subject.Math).ToString());
                    writer.WriteElementString("Physics", group.SubjectMode(Exam.Subject.Physics).ToString());
                    writer.WriteElementString("English", group.SubjectMode(Exam.Subject.English).ToString());
                    writer.WriteEndElement(); // Modes

                    writer.WriteStartElement("Examinees");
                    foreach (Examinee examinee in group.Examinees)
                    {
                        writer.WriteStartElement("Examinee");
                        writer.WriteElementString("Name", examinee.Name);
                        writer.WriteElementString("WeightedAverage", examinee.ResultAverage().ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement(); // Examinees
                    writer.WriteEndElement(); // Group
                }
                writer.WriteEndElement(); // Groups
                writer.WriteEndElement(); // Exam

                writer.WriteStartElement("Errors");
                foreach (string error in errors)
                {
                    writer.WriteElementString("Error", error);
                }
                writer.WriteEndElement(); // Errors

                writer.WriteEndElement(); // Report
                writer.WriteEndDocument();
            } 
        }
    }
}
