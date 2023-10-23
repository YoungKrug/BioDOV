using System;
using Accord.Diagnostics;
using Accord.Math;
using Accord.Statistics;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression.Linear;
using Accord.Statistics.Testing;
using Debug = UnityEngine.Debug;

namespace _Scripts.Statistics
{
    public static class GrangerCausalityTestingModel
    {
        public enum GrangerRelationship : int
        {
            Bidirectional = 0,
            Directional = 1
        };
        public static GrangerRelationship IsGrangerCausal(double[] seriesA, double[] seriesB, int maxLag = 10)
        {
            int aBSignificantCounter = 0; //If A and B are significant, Bi-direction relationship
            int aSignificantCounter = 0; // If just A is significant, A Granger B
            // Iterate over lags
            for (int lag = 1; lag <= maxLag; lag++)
            {
                // Extract lagged values
                double[] laggedA = new double[seriesA.Length - lag];
                double[] laggedB = new double[seriesB.Length - lag];
                Array.Copy(seriesA, lag, laggedA, 0, laggedA.Length);
                Array.Copy(seriesB, lag, laggedB, 0, laggedB.Length);

                double[] outputA = seriesA[lag..];
                double[] outputB = seriesB[lag..];
                // Fit autoregressive models
                double alphaA, betaA;
                int degreesOfFreedomA, degreesOfFreedomB;
                FitAutoregressiveModel(laggedA, outputA, out alphaA, out betaA, out degreesOfFreedomA);

                double alphaB, betaB;
                FitAutoregressiveModel(laggedB, outputB, out alphaB, out betaB, out degreesOfFreedomB);
                // Perform comparison (you might use statistical tests here)
                //To calculate residuals (for predictions)
                // Y(t) = alpha + Beta(1) * Y(t-1)
                double[] predictedListA = new double[outputA.Length];
                for (int i = 1; i < outputA.Length; i++)
                {
                    predictedListA[i] = alphaA + betaA * outputA[i - 1]; //output A is already lagged at time
                }
                
                double[] predictedListB = new double[outputB.Length];
                for (int i = 1; i < outputB.Length; i++)
                {
                    predictedListB[i] = alphaB + betaB * outputB[i - 1]; //output A is already lagged at time
                }

                ChiSquareTest chiSquareModelA = new ChiSquareTest(outputA, predictedListA, degreesOfFreedomA);
                ChiSquareTest chiSquareModelB = new ChiSquareTest(outputB, predictedListB, degreesOfFreedomB);
                switch (chiSquareModelA.Significant)
                {
                    case true when chiSquareModelB.Significant:
                        aBSignificantCounter++;
                        break;
                    case true:
                        aSignificantCounter++;
                        break;
                }
            }
            Debug.Log($"Evidence: AB significance: {aBSignificantCounter}/{maxLag}, " +
                      $"A Significance: {aSignificantCounter}/{maxLag}");
            // No evidence of Granger causality
            return aBSignificantCounter > aSignificantCounter? GrangerRelationship.Bidirectional : GrangerRelationship.Directional;
        }
        
        private static void FitAutoregressiveModel(double[] lagged, double[] series, out double alpha, out double beta,
            out int degreesOfFreedom)
        {
            // Fit a simple autoregressive model (y = alpha + beta * lagged + epsilon)
            double[][] input = new double[lagged.Length][];
            for (int i = 0; i < lagged.Length; i++)
            {
                input[i] = new double[] { lagged[i] };
            }

            var regression = new OrdinaryLeastSquares();
            var num = lagged.Length..;
            var model = regression.Learn(input, series);
            // Extract coefficients
            alpha = model.Intercept;
            beta = model.Weights[0];
            degreesOfFreedom = (int)model.GetDegreesOfFreedom(input.Length);
        }
    }
}