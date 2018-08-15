using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExaminationProcessor
{
    class Group
    {
        public string Name { get; set; }
        public List<Examinee> Examinees { get; set; } = new List<Examinee>();

        public double SubjectAverage(Exam.Subject subject)
        {
            return Examinees.Average(r => r.Results[(int)subject]); ;
        }

        public double SubjectMedian(Exam.Subject subject)
        {
            double result;
            var ordered = Examinees.OrderBy(r => r.Results[(int)subject]);
            if (Examinees.Count % 2 == 0)
            {
                result = (double)ordered.ElementAt(Examinees.Count / 2).Results[(int)subject] 
                       + ordered.ElementAt((Examinees.Count / 2) - 1).Results[(int)subject];
                result /= 2;
            }
            else
            {
                result = ordered.ElementAt(Examinees.Count / 2).Results[(int)subject];
            }

            return result;
        }

        public byte SubjectMode(Exam.Subject subject)
        {
            var grouped = Examinees.GroupBy(r => r.Results[(int)subject]);
            var orderedByCount = grouped.OrderByDescending(r => r.Count());
            return orderedByCount.First().Key;
        }
    }
}
