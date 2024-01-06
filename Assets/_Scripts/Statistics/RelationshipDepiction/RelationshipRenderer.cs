using UnityEngine;

namespace _Scripts.Statistics.RelationshipDepiction
{
    public class RelationshipRenderer
    {
        public void ConnectLines(LineRenderer firstLine, Gradient gradient)
        {
            firstLine.colorGradient = gradient;
        }
    }
}