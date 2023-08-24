using System.IO;
using _Scripts.CSVData;
using _Scripts.Statistics;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class StatisticsTests
    {
        private Csv _csv;
        private PartialLeastSquaresPredictionModel _model;
        [SetUp]
        public void Setup()
        {
            string path = Path.Combine(Application.dataPath, "SimulationFiles/DataExtra.csv");
            FileInserter inserter = new FileInserter(path);
            _csv = inserter.ReadData();
            _model = new PartialLeastSquaresPredictionModel(_csv.Data[0], _csv);
        }
        [Test]
        public void Predict()
        {
            double prediction = _model.Predict(_csv.Data[6].States.ToArray());
            Assert.GreaterOrEqual(prediction, 0);
        }
    }
}