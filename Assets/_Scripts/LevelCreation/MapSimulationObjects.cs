using System.Collections.Generic;
using System.Text;
using _Scripts.Documentation;
using _Scripts.Simulation;
using _Scripts.Statistics;
using UnityEngine;

namespace _Scripts.LevelCreation
{
    public class MapSimulationObjects
    {
        private readonly string path = "C:/Users/gregj/Documents/relationship.txt";
        private readonly RelationshipStatisticalAnalysisModel _model = new RelationshipStatisticalAnalysisModel();
        private readonly List<LineRenderer> _lineRenderers = new List<LineRenderer>();
        /// <summary>
        /// TODO, Map the position of the nodes properly, we need to ensure there is a clear distinction
        ///  related to which nodes are related and which ones arent. 
        /// </summary>
        public void MapBasedOnRelationship(SimulationConfig config)
        {
            CleanUp();
            FileWriter writer = new FileWriter();
            StringBuilder stringBuilder = new StringBuilder();
            Dictionary<string, double> relationshipDictionary = new Dictionary<string, double>();
            GameObject lineRenderer = Resources.Load("Node-Connections/Line") as GameObject;
            Color positiveRelationship = Color.green;
            Color negativeRelationship = Color.red;
            Color noRelationship = Color.white;
            const string delimiter = "->";
            foreach (var simObj in config.Data.AllCurrentObjects)
            {
                foreach (var otherObj in config.Data.AllCurrentObjects)
                {
                    if(simObj.Node.Name.Equals(otherObj.Node.Name))
                        continue;
                    string key = $"{simObj.Node.Name}{delimiter}{otherObj.Node.Name}";
                    string oppositeKey = $"{otherObj.Node.Name}{delimiter}{simObj.Node.Name}";
                    Debug.Log(key);
                    double relationship = _model.AnalysisRelationship(simObj.Node.States,
                        otherObj.Node.States);
                    bool isGranger = GrangerCausalityTestingModel.IsGrangerCausal(simObj.Node.States.ToArray(),
                        otherObj.Node.States.ToArray());
                    bool isGrangerOpp = GrangerCausalityTestingModel.IsGrangerCausal(otherObj.Node.States.ToArray(),
                        simObj.Node.States.ToArray());
                   // Debug.Log($"{simObj.Node.Name} is Granger: {isGranger} to {otherObj.Node.Name}");
                    //Debug.Log($"{otherObj.Node.Name} is Granger: {isGrangerOpp} to {simObj.Node.Name}");
                  //  double relationship1 = _model.AnalysisRelationship(otherObj.Node.States,
                     //   simObj.Node.States);
                    //Debug.Log($"Relation 1: {relationship}, opposite relation: {relationship1}");
                    string verbose = $"{simObj.Node.Name} and {otherObj.Node.Name} Relationship: {relationship}\n";
                    Vector3 positionVector = new Vector3(0, (float)relationship, (float)relationship);
                    simObj.gameObject.transform.position += positionVector;
                    if(!relationshipDictionary.ContainsKey(oppositeKey))
                        relationshipDictionary.Add(key, relationship);
                    stringBuilder.Append(verbose);
                }
            }

            foreach (var simObj in config.Data.AllCurrentObjects)
            {
                foreach (var otherObj in config.Data.AllCurrentObjects)
                {
                    if(simObj.Node.Name.Equals(otherObj.Node.Name))
                        continue;
                    string key = $"{simObj.Node.Name}{delimiter}{otherObj.Node.Name}";
                    if(!relationshipDictionary.ContainsKey(key))
                        continue;
                    double relationship = relationshipDictionary[key];
                    LineRenderer line = Object.Instantiate(lineRenderer).GetComponent<LineRenderer>();
                    line.SetPositions(new Vector3[]
                    {
                        simObj.transform.position,
                        otherObj.transform.position
                    });
                    line.startColor = relationship > 0 ? positiveRelationship : negativeRelationship;
                    if (relationship == 0)
                        line.startColor = noRelationship;
                    _lineRenderers.Add(line);
                }
            }
            writer.WriteToFile(path, stringBuilder.ToString());
        }
        private void CleanUp()
        {
            foreach (var line in _lineRenderers)
            {
                Object.Destroy(line);
            }

            _lineRenderers.Clear();
        }
    }
}