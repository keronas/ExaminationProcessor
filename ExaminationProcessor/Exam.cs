using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExaminationProcessor
{
    class Exam
    {
        public enum Subject {Math = 0, Physics = 1, English = 2}
        public static readonly double[] SubjectWeights = {40, 35, 25};

        public string Name { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();

        public double SubjectAverage(Exam.Subject subject)
        {
            var examinees = Groups.SelectMany(r => r.Examinees);
            return examinees.Average(r => r.Results[(int)subject]); ;
        }

        public double SubjectMedian(Exam.Subject subject)
        {
            double result;
            var examinees = Groups.SelectMany(r => r.Examinees);
            var ordered = examinees.OrderBy(r => r.Results[(int)subject]);
            if (examinees.Count() % 2 == 0)
            {
                result = (double)ordered.ElementAt(examinees.Count() / 2).Results[(int)subject]
                       + ordered.ElementAt((examinees.Count() / 2) - 1).Results[(int)subject];
                result /= 2;
            }
            else
            {
                result = ordered.ElementAt(examinees.Count() / 2).Results[(int)subject];
            }

            return result;
        }

        public byte SubjectMode(Exam.Subject subject)
        {
            var examinees = Groups.SelectMany(r => r.Examinees);
            var grouped = examinees.GroupBy(r => r.Results[(int)subject]);
            var orderedByCount = grouped.OrderByDescending(r => r.Count());
            return orderedByCount.First().Key;
        }
    }
}
