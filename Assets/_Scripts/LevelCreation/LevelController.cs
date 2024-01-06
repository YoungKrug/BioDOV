using System;
using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Event;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation;
using UnityEngine;

namespace _Scripts.LevelCreation
{
    public class LevelController : MonoBehaviour, IEventReactor
    {
        public BaseEventScriptableObject levelEventScriptableObject;
        public BaseEventScriptableObject simulatorEventObject;
        public List<LevelDataScriptableObject> LevelsList = new List<LevelDataScriptableObject>();
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
            if (_level + 1 > LevelsList.Count)
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
    }
}