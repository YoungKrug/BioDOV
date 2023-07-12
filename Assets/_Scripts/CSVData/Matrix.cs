using System.Text;
namespace _Scripts.CSVData
{
    public class Matrix<T>
    {
        public Matrix(int row, int column)
        {
            MatrixData = new T[column, row];
            _rowlength = row;
            _columnlength = column;
        }
        public T[,] MatrixData;
        private int _rowlength;
        private int _columnlength;
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _columnlength; i++)
            {
                for (int j = 0; j < _rowlength; j++)
                {
                    stringBuilder.Append(MatrixData[i, j]);
                    stringBuilder.Append(",");
                }

                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}