using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ThresholdMeter
    {
        
        private readonly Image _image;
        public ThresholdMeter(Image image)
        {
            _image = image;
        }
        public double CalculateThreshold(double[] states)
        {
            int numberOfZeroStates = 0;
            foreach (var state in states)
            {
                if (state == 0d)
                {
                    numberOfZeroStates++;
                }
            }
            double threshold = numberOfZeroStates / (double)states.Length;
            _image.fillAmount = (float)threshold;
            return threshold;
        }
    }
}