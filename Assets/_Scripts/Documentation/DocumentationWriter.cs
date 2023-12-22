using System.Collections.Generic;
using System.Text;

namespace _Scripts.Documentation
{
    public class DocumentationWriter
    {
        private readonly Stack<string> _documentationData = new Stack<string>();
        private readonly StringBuilder _data = new StringBuilder();
        public bool AddData(string data)
        {
            _documentationData.Push(data);
            return true;
        }
        public bool UndoLastData()
        {
            if(_documentationData.Count > 0)
                _documentationData?.Pop();
            return true;
        }
        public override string ToString()
        {
            while (_documentationData.Count > 0)
            {
                string docString = _documentationData.Pop();
                _data.Append(docString);
            }
            // Write to file
            return _data.ToString();
        }
    }
}