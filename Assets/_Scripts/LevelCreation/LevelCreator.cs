using _Scripts.CSVData;

namespace _Scripts.LevelCreation
{
    public class LevelCreator
    {
        private readonly Csv _csv;
        public LevelCreator(Csv csv)
        {
            _csv = csv;
        }
        public Csv CreateLevel(LevelConfig config)
        {
            Csv csv = new Csv();
            for (int i = 0; i < config.numberOfNodes; i++)
            {
                CsvNode node = _csv.Data[i];
                node.CurrentState = config.nodeStateState;
                csv.Data.Add(node);
            }
            return csv;
        }
    }
}