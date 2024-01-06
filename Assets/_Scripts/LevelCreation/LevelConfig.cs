using System.Collections.Generic;

namespace _Scripts.LevelCreation
{
    [System.Serializable]
    public class LevelConfig
    {
        public int numberOfNodes; // Nodes to be created
        public float thresholdToCompleteLevel; // Threshold of nodes that need to be at a stable state to complete the level
        public int nodeStateState; // The start state of all the nodes
    }
}