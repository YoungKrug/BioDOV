using System.Collections.Generic;
using System.Linq;
using Accord.Statistics.Testing;
using MathNet.Numerics.Statistics;
using UnityEngine;

namespace _Scripts.Statistics
{
    public class RelationshipStatisticalAnalysisModel
    {
        private double CalculateCorrelation(IEnumerable<double> dataOne, IEnumerable<double> dataTwo)
        {
            return Correlation.Pearson(dataOne, dataTwo);
        }

        private double CovarianceTest(IEnumerable<double> dataOne, IEnumerable<double> dataTwo)
        {
            return dataOne.Covariance(dataTwo);
        }

        private double CrossCorrelation(List<double> dataOne, List<double> dataTwo)
        {
            return CrossCorrelationAnalysisModel.CalculateCrossCorrelation(dataOne.ToArray(), dataTwo.ToArray());
        }

        public double AnalysisRelationship(List<double> initialData, List<double> otherData)
        {
           // Accord.Statistics.Testing.MannWhitneyWilcoxonTest wilcoxonTest =
             //   new MannWhitneyWilcoxonTest(initialData.ToArray(), otherData.ToArray());
          //  wilcoxonTest.
            //Lets apply weights
            List<double> variances = new List<double>
            {
                CovarianceTest(initialData, otherData),
                CalculateCorrelation(initialData, otherData)
            };
            //Weighted measure
            const double weightOne = 0.25d;
            const double weightTwo = 0.75d;

            double weightedRes = (weightOne * variances[0]) + (weightTwo * variances[1]);
            //Debug.Log($"{variances[0]}, and {variances[1]}: {weightedRes}");
            double result = weightedRes;
            return double.IsNaN(result) ? 0 : result;
        }
        
    }
}