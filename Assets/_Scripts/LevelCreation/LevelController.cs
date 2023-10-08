using System;
using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Event;
using _Scripts.Interface;
using _Scripts.Planning;
using _Scripts.Planning.Interfaces;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation;
using UnityEngine;

namespace _Scripts.LevelCreation
{
    public class LevelController : MonoBehaviour, IEventReactor, IPlannable
    {
        public BaseEventScriptableObject levelEventScriptableObject;
        public BaseEventScriptableObject simulatorEventObject;
        public List<LevelDataScriptableObject> LevelsList = new List<LevelDataScriptableObject>();
        public int Order => 1;
        private int _level = 0;
        private LevelCreator _levelCreator;
        private bool _isInit = false;

        private void Awake()
        {
            levelEventScriptableObject.Subscribe(this);
        }

        private void Init(Csv csv)
        {
            if (!_isInit)
            {
                _isInit = true;
                _levelCreator = new LevelCreator(csv);
            }
        }

        private void CreateLevel()
        {
            if (_level > LevelsList.Count)
                _level = 0;
            Csv newCsv = _levelCreator.CreateLevel(LevelsList[_level++].levelConfig);
            simulatorEventObject.OnEventRaised(newCsv);
        }

        public void Execute(object obj)
        {
            if (obj is Csv)
            {
                Csv csv = (Csv)obj;
                Init(csv);
                CreateLevel();
            }
            else
            {
                CreateLevel();
            }
        }
        
        public bool CheckForPrerequisite(object obj)
        {
            return obj.GetType() == typeof(Csv);
        }

        public PlannerConfig PlannedExecution(PlannerConfig plannerConfig)
        {
            object preObject = plannerConfig.InvolvedObject;
            if (!CheckForPrerequisite(preObject))
            {
                plannerConfig.PlannerState = Planner.PlannerState.PlanFailed;
                return null;
            }
            Csv csv = (Csv)preObject;
            Init(csv);
            CreateLevel();
            plannerConfig.InvolvedObject = csv;
            return plannerConfig;
        }
        public string GetName()
        {
            return this.name;
        }
    }
}