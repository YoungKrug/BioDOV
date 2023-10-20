using System;
using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Statistics
{
    public class CrossCorrelationAnalysisModel
    {
        public double CalculateCrossCorrelation(double[] signalOne, double[] signalTwo)
        {
            int sOneLength = signalOne.Length; //N
            int sTwoLength = signalTwo.Length; //M
            double normalizedMagnitude = Magnitude(signalOne) * Magnitude(signalTwo);
            //signalOne is F and SignalTwo is G
            List<double> correlationAtTimeLag = new List<double>();
            for (int i = 0; i < sOneLength; i++)
            {
                int timeLag = i; //K
                double correlationAtTime = 0;
                for (int j = 0; j < sOneLength; j++)
                {
                    //i represents timelag
                    double signalOneValue = signalOne[j];
                    double signalTwoValue = signalTwo[(j + timeLag) % sTwoLength];
                    correlationAtTime += (signalOneValue * signalTwoValue);
                }

                double normalizedCorrelation = correlationAtTime / normalizedMagnitude;
                correlationAtTimeLag.Add(normalizedCorrelation);
            }
            return 0;
        }

        private double Magnitude(double[] arr)
        {
            return Math.Sqrt(arr.Sum(element => Math.Pow(element, 2)));
        }
    }
}