using System.IO;
using _Scripts.CSVData;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class CsvDataTest
    {
        public DataInserter Inserter;
        [Test]
        [TestCase("SimulationFiles/DataExtra.csv")]
        public void TestInserter(string path)
        {
            path = Path.Combine(Application.dataPath, path); 
            FileInserter fileInserter = new FileInserter(path);
            Csv csv = fileInserter.ReadData();
            Assert.True(csv.Data.Count > 0);
        }

        [SetUp]
        public void Setup()
        {
            GameObject gameObject = new GameObject();
            Inserter = gameObject.AddComponent<DataInserter>();
        }

        [Test]
        [TestCase("SimulationFiles/DataExtra.csv")]
        public void TestDataInserter(string path)
        {
            path = Path.Combine(Application.dataPath, path); 
            bool isTrue = Inserter.OnInsertEvent(path);
            Assert.True(isTrue);
        }
    }
}