﻿using System.Collections.Generic;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using _Scripts.Simulation.SimulationSettings;
using UnityEngine;

namespace _Scripts.Simulation
{
    public class CustomSimulation : ISimulator
    {
        [SerializeField] private BaseEventScriptableObject _baseEventScriptableObject;
        private SimulationData _simulationData;

        public bool Simulate(SimulationConfig config)
        {
            return true;
        }

        public bool ExecuteCommand(List<ICommand> commands, SimulationObject simulationObject)
        {
            return true;
        }

        public bool UndoCommand()
        {
            return true;
        }
    }
}