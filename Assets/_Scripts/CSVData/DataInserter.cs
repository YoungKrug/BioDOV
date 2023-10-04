using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Scripts.Planning;
using _Scripts.Planning.Interfaces;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CSVData
{
    public class DataInserter : MonoBehaviour, IPlannable
    {
        // Start is called before the first frame update
        
        private Csv _csv;
        public int Order => 0;
        public BaseEventScriptableObject levelEventScriptableObject;
        private void Start()
        {
            string path = Path.Combine(Application.dataPath, "SimulationFiles/DataExtra.csv");
            OnInsertEvent(path);
        }

        public bool OnInsertEvent(string path)
        {
            FileInserter inserter = new FileInserter(path);
            _csv = inserter.ReadData();
            if(levelEventScriptableObject)
                levelEventScriptableObject.OnEventRaised(_csv);
            return _csv.Data.Count > 0;
        }
        
       
        public bool CheckForPrerequisite(object obj)
        {
            //This class has no prerequisites 
            return true;
        }

        public PlannerConfig PlannedExecution(PlannerConfig plannerConfig)
        {
            //Ask for path
            if (!CheckForPrerequisite(plannerConfig))
            {
                plannerConfig.PlannerState = Planner.PlannerState.PlanFailed;
                return null;
            }
            string path = Path.Combine(Application.dataPath, "SimulationFiles/DataExtra.csv");
            OnInsertEvent(path);
            plannerConfig.InvolvedObject = _csv;
            return plannerConfig;
        }
    }
}
