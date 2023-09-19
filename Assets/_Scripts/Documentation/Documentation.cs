using System.Text;

namespace _Scripts.Documentation
{
    public class Documentation
    {
        private StringBuilder _data;

        public void AddData(string data)
        {
            _data.Append(data);
        }

        public override string ToString()
        {
            return _data.ToString();
        }
    }
}