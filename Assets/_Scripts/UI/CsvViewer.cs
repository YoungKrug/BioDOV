using System.Collections.Generic;
using System.Text;
using _Scripts.CSVData;
using UnityEngine;

namespace _Scripts.UI
{
    public class CsvViewer
    {
        private readonly List<CsvNode> _nodes;
        private readonly CsvUINode _templateNodeDropDown;
        private readonly GameObject _parent;
        private readonly List<string> _columnStrings;
        private List<CsvUINode> _createdUINodes = new List<CsvUINode>();
        private const int NumberOfColumns = 10;

        public CsvViewer(List<CsvNode> nodes, CsvUINode templateNodeDropDown, GameObject parent)
        {
            _nodes = nodes;
            _templateNodeDropDown = templateNodeDropDown;
            _parent = parent;
            PopulateColumnData();
        }

        private void DeleteCurrentUINodes()
        {
            for (int i = _createdUINodes.Count; i > 0; i--)
            {
                Object.Destroy(_createdUINodes[i]);
            }
            _createdUINodes.Clear();
        }
        private void PopulateColumnData()
        {
            //Lets create a giant string
            foreach (var node in _nodes)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < NumberOfColumns; i++)
                {
                    stringBuilder.Append($"{node.States[i]}\n");
                }
                _columnStrings.Add(stringBuilder.ToString());
            }
        }
        public void DisplayNodes()
        {
            foreach (var columnString in _columnStrings)
            {
                CsvUINode nodeUI = Object.Instantiate(_templateNodeDropDown, _parent.transform);
                nodeUI.text.text = columnString;
                _createdUINodes.Add(nodeUI);
            }
        }
    }
}