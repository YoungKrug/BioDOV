using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.CSVData;
using _Scripts.Interface;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class SimulationInvokerTest
    {
        private SimulationInvoker _invoker;
        private SimulationData _data;
        private SimulationObject _simulationObject;
        [SetUp]
        public void Setup()
        {
            _simulationObject = Resources.Load<SimulationObject>("Testing/Node_Test");
            _simulationObject.Node = new CsvNode
            {
                States = new List<double> { 0, 1, 2 },
                CurrentState = 0
            };
            _data = new SimulationData
            {
                AllCurrentObjects = new List<SimulationObject>{_simulationObject},
                CurrentStates = new double[] { 0, 1}
            };
            _invoker = new SimulationInvoker();
        }
        [Test]
        public void TestExecuteCommand()
        {
            List<ICommand> commands = new List<ICommand> { new ClickDetectionCommand() };
            SimulationData returnedData = _invoker.ExecuteCommand(commands, ref _data);
            Assert.IsNotNull(returnedData);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void TestUndoCommand(bool hasCommandsOnStack)
        {
            if (hasCommandsOnStack)
            {
                _invoker.ExecuteCommand(new List<ICommand> { new ChangeColorBasedOnStatesCommand() }, ref _data);
                _invoker.ExecuteCommand(new List<ICommand> { new ClickDetectionCommand() }, ref _data);
            }
            bool isSuccessful = _invoker.UndoCommands(ref _data);
            Assert.AreEqual(isSuccessful, hasCommandsOnStack);
        }
    }
}