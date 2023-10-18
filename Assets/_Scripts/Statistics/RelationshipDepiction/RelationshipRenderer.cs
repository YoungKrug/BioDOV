using UnityEngine;

namespace _Scripts.Statistics.RelationshipDepiction
{
    public class RelationshipRenderer
    {
        public void ConnectLines(LineRenderer firstLine, LineRenderer otherLine)
        {
            Vector3 firstPositionInitial = firstLine.GetPosition(0);
            Vector3 firstPositionEnd = firstLine.GetPosition(1);
            Vector3 midPoint = MidPoint(firstPositionInitial, firstPositionEnd);
            //From Midpoint to inital is the new first position
            firstLine.SetPositions(new Vector3[] {firstPositionInitial, midPoint});
            otherLine.SetPositions(new Vector3[] {midPoint, firstPositionEnd});
        }
        private Vector3 MidPoint(Vector3 first, Vector3 other)
        {
            float xFirst = first.x;
            float yFirst = first.y;
            float zFirst = first.x;

            float xOther = other.x;
            float yOther = other.y;
            float zOther = other.z;

            return new Vector3((xFirst + xOther) / 2, (yFirst + yOther) / 2, (zFirst + zOther) / 2);

        }
    }
}