using System.Collections;
using System.Collections.Generic;
using _Scripts.Commands;
using _Scripts.Interface;
using _Scripts.Simulation;
using _Scripts.Simulation.SimulationSettings;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CsvTesting
{
    // A Test behaves as an ordinary method
    private SimulationData _simulationData;
    [SetUp]
    public void CreateData()
    {
        _simulationData = new SimulationData
        {
            CurrentStates = new double[20],
            AllCurrentObjects = new List<SimulationObject>(20)
        };
        //DefaultSimulation
    }
    [Test]
    public void CsvTestingSimplePasses()
    {
        SimulationInvoker invoker = new SimulationInvoker();
        SimulationData data = new SimulationData();
        data.CurrentStates = new double[20];
        data.AllCurrentObjects = new List<SimulationObject>(20);
        List<ICommand> commands = new List<ICommand> {new ClickDetectionCommand()};
        //invoker.ExecuteCommand(commands, )
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CsvTestingWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
