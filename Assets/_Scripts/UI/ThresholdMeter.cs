using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ThresholdMeter
    {
        
        private readonly Slider _slider;
        public ThresholdMeter(Slider slider)
        {
            _slider = slider;
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
            _slider.value = (float)threshold;
            return threshold;
        }
    }
}