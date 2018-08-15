using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExaminationProcessor
{
    class Examinee
    {
        public string Name { get; set; }
        public byte[] Results { get; set; } = new byte[Enum.GetNames(typeof(Exam.Subject)).Length];

        public double ResultAverage()
        {
            double sum = 0;
            double divisor = 0;
            for(int i = 0; i < Results.Length; i++)
            {
                divisor += Exam.SubjectWeights[i];
                sum += Results[i] * Exam.SubjectWeights[i];
            }
            return sum/divisor;
        }
    }
}
