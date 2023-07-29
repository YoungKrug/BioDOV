using System.IO;
using System.Text;
using _Scripts.Statistics;


namespace _Scripts.CSVData
{
    public class AnalysisCsvData
    {
        public string path = "Assets/SimulationFiles/";
        
        public void PartialLeastSquareRegression(Csv csv)
        {
            PartialLeastSquaresPredictionModel model = new PartialLeastSquaresPredictionModel(csv.Data[0], csv);
        }
    
        public void CreateTextFile(string text, string fileName)
        {
            string file = $"{path}/{fileName}.csv";
            using (FileStream fs = File.Create(file))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(text);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}