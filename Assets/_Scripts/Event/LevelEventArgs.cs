using _Scripts.CSVData;
using _Scripts.Interface;

namespace _Scripts.Event
{
    public class LevelEventArgs
    {
        public ISimulator Simulator;
        public Csv Csv;
    }
}