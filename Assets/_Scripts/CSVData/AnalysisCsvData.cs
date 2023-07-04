using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.CSVData
{
    public class AnalysisCsvData
    {
        // I have to sift through the data and look for changes in iteration. Essentially, if A(x) changes in
        // I(Iteration), and B(x) also changes from the I -> I - 1, how does that affect the data.
        //Did B(x) affect A(x) or is there another variable that changes it. 
        //Going to look through all of it to find out similarities
        public void FindCasualtyInData(Csv csv)
        {
            List<CasualtyInformation> casualtyPercentageArray = new List<CasualtyInformation>();
            int count = csv.Data.Count;
            for (int i = 0; i < count; i++) //O(N^3) Needs to be reduced to O(N^2) or O(NLogN) if possible
            {
                CsvNodes currentNode = csv.Data[i];
              
                foreach (var otherNode in csv.Data)
                {
                    if(currentNode.Name == otherNode.Name)
                        continue;
                    string currentNodePreviousState = "";
                    string otherNodePreviousState = "";
                    float casualtyPercentage = 0;
                    int stateCount = otherNode.States.Count;
                    for (int j = 0; j < stateCount; j++)
                    {
                        string currentState = currentNode.States[j];
                        string otherState = otherNode.States[j];
                        if (currentNodePreviousState == "" && otherNodePreviousState == "")
                        {
                            currentNodePreviousState = currentState;
                            otherNodePreviousState = otherState;
                            continue;
                        }
                        // We need to determine states, HIGHLY EXPRESSED, NEUTRAL, LOW EXPRESSION
                        // 
                        
                        //Did my state change?
                        bool didCurrentNodeChange = currentState == currentNodePreviousState;
                        bool didOtherNodeChane = otherState == otherNodePreviousState;

                        if (didCurrentNodeChange && didOtherNodeChane)
                        {
                            casualtyPercentage  += (1.0f / stateCount);
                        }
                      
                        //If the other node changed and mine didnt, no causation,
                        //If my node changed and the other did not, no causation
                        

                    }
                    CasualtyInformation casualtyItem = new CasualtyInformation
                    {
                        Name = otherNode.Name,
                        CasualtyPercent = casualtyPercentage
                    };

                    if (currentNode.CasualtyInformationList.All(x => x.Name != otherNode.Name))
                    {
                        currentNode.CasualtyInformationList.Add(casualtyItem);
                    }

                }
            }
            Debug.Log("done");
        }
        
        
    }
}