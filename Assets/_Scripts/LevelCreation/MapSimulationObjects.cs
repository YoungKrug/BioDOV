using System.Collections.Generic;
using System.Text;
using _Scripts.Documentation;
using _Scripts.Simulation;
using _Scripts.Statistics;
using _Scripts.Statistics.RelationshipDepiction;
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
            Dictionary<string,   GrangerCausalityTestingModel.GrangerRelationship> relationshipDictionary = new Dictionary<string,   GrangerCausalityTestingModel.GrangerRelationship>();
            Dictionary<string, double> quantitativeRelationshipDictionary = new Dictionary<string, double>();
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
                    GrangerCausalityTestingModel.GrangerRelationship isGranger = GrangerCausalityTestingModel.IsGrangerCausal(simObj.Node.States.ToArray(),
                        otherObj.Node.States.ToArray());
                    Debug.Log($"{simObj.Node.Name} is {isGranger.ToString()} to {otherObj.Node.Name}");
                    string verbose = $"{simObj.Node.Name} and {otherObj.Node.Name} Relationship: {relationship}\n";
                    Vector3 positionVector = new Vector3(0, (float)relationship, (float)relationship);
                    simObj.gameObject.transform.position += positionVector;
                    if (!relationshipDictionary.ContainsKey(oppositeKey))
                        relationshipDictionary.Add(key, isGranger);
                    quantitativeRelationshipDictionary.Add(key, relationship);
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
                    GrangerCausalityTestingModel.GrangerRelationship relationship = relationshipDictionary[key];
                    double quantitativeRelationship = quantitativeRelationshipDictionary[key];
                    LineRenderer line = Object.Instantiate(lineRenderer).GetComponent<LineRenderer>();
                    RelationshipRenderer relationshipRenderer = new RelationshipRenderer();
                    Vector3 simObjPos = simObj.transform.position;
                    Vector3 otherObjPos = otherObj.transform.position;
                    Gradient gradient = config.lineGradient;
                    if (relationship == GrangerCausalityTestingModel.GrangerRelationship.Bidirectional)
                    {
                        line.SetPositions(new Vector3[]
                        {
                            simObjPos,
                            otherObjPos
                        });
                        line.startColor = quantitativeRelationship > 0 ? positiveRelationship : negativeRelationship;
                    }
                    else if (relationship == GrangerCausalityTestingModel.GrangerRelationship.Directional)
                    {
                        LineRenderer otherLine = Object.Instantiate(lineRenderer).GetComponent<LineRenderer>();
                        line.SetPositions(new Vector3[]
                        {
                            simObjPos,
                            otherObjPos
                        });
                        gradient.colorKeys[1].color = Color.white;
                        gradient.colorKeys[0].color = quantitativeRelationship > 0 ? positiveRelationship : negativeRelationship;
                        line.colorGradient = gradient;
                        relationshipRenderer.ConnectLines(line, gradient);
                        
                    }
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