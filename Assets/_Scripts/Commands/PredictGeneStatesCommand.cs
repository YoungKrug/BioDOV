
using System.Collections.Generic;
using _Scripts.Interface;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;

namespace _Scripts.Commands
{
    public class PredictGeneStatesCommand : ICommand // Concrete Commands
    {
        
        public SimulationData Data { get; private set; }
        public void Execute()
        {
            foreach (var simulatedObject in Data.AllCurrentObjects)
            {
                if (simulatedObject.hasInteracted)
                {
                    simulatedObject.hasInteracted = false;
                    continue;
                }
                double newState = simulatedObject.Node.PredictionModel.Predict(Data.CurrentStates);
                simulatedObject.Node.CurrentState = newState;
            }
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }

        public void Set(SimulationData data)
        {
            throw new System.NotImplementedException();
        }
    }
}