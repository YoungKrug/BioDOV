using System.Text;
using _Scripts.Documentation;
using _Scripts.Simulation;
using _Scripts.Statistics;
using UnityEngine;

namespace _Scripts.LevelCreation
{
    public class MapSimulationObjects
    {
        private readonly SimulationConfig _config;
        private readonly string path = "C:/Users/gregj/Documents/relationship.txt";
        private readonly RelationshipStatisticalAnalysisModel _model = new RelationshipStatisticalAnalysisModel();
        public MapSimulationObjects(SimulationConfig config)
        {
            _config = config;
        }
        /// <summary>
        /// TODO, Map the position of the nodes properly, we need to ensure there is a clear distinction
        ///  related to which nodes are related and which ones arent. 
        /// </summary>
        public void MapBasedOnRelationship()
        {
            FileWriter writer = new FileWriter();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var simObj in _config.Data.AllCurrentObjects)
            {
                foreach (var otherObj in _config.Data.AllCurrentObjects)
                {
                    if(simObj.Node.Name.Equals(otherObj.Node.Name))
                        continue;
                    double relationship = _model.AnalysisRelationship(simObj.Node.States,
                        otherObj.Node.States);
                    string verbose = $"{simObj.Node.Name} and {otherObj.Node.Name} Relationship: {relationship}\n";
                    Vector3 positionVector = new Vector3(0, (float)relationship);
                    simObj.gameObject.transform.position += positionVector;
                    stringBuilder.Append(verbose);
                }
            }
            writer.WriteToFile(path, stringBuilder.ToString());
        }
    }
}