using System.Collections.Generic;
using System.Linq;
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

        public double AnalysisRelationship(List<double> initialData, List<double> otherData)
        {
            //Lets apply weights
            List<double> variances = new List<double>
            {
                CovarianceTest(initialData, otherData),
                CalculateCorrelation(initialData, otherData)
            };
            //Weighted measure
            double weightOne = 0.45d;
            double weightTwo = 0.55d;

            double weightedRes = (weightOne * variances[0]) + (weightTwo * variances[1]);
            Debug.Log($"{variances[0]}, and {variances[1]}: {weightedRes}");
            double result = weightedRes;
            return double.IsNaN(result) ? 0 : result;
        }
        
    }
}