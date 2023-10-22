using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Scripts.Documentation;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Accord.MachineLearning;
using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace _Scripts.Statistics
{
    public static class CrossCorrelationAnalysisModel
    {
        public static double CalculateCrossCorrelation(double[] signalOne, double[] signalTwo)
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
            return correlationAtTimeLag.Average();
        }

        private static double Magnitude(double[] arr)
        {
            return Math.Sqrt(arr.Sum(element => Math.Pow(element, 2)));
        }

        public static void Varmachine()
        {
            // Sample data
            double[] input = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double[] output = { 2, 4, 6, 8, 10, 12, 14, 16, 18 };

            // Create a support vector machine (SVM) for regression
           // var machine = new SupportVectorMachine<Gaussian>(input.Lengt h, );

            // Create a learning algorithm
            var teacher = new SequentialMinimalOptimization<Gaussian>()
            {
                Complexity = 100 // Adjust complexity parameter
            };

            // Train the machine
        //     var error = teacher.Learn(machine, input, output);
        //
        //     // Get the estimated parameters
        //     double alpha = machine.Weights[0]; // Intercept
        //     double beta1 = machine.Weights[1]; // Coefficient for lagged value
        //
        //     // Output the results
        //     Console.WriteLine($"Intercept (alpha): {alpha}");
        //     Console.WriteLine($"Coefficient for lagged value (beta1): {beta1}");
        }
    }
}