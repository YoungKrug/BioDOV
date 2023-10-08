using System.Collections.Generic;
using System.Linq;
using _Scripts.Interface;
using _Scripts.Planning.Interfaces;
using _Scripts.ScriptableObjects;

namespace _Scripts.Planning
{
    // THe items involved in the action
    // The Precondictions, facts that must be true for the action to run
    // And how it effects the world after completing the interaction
    public class Planner : IEventReactor
    {

        private BaseEventScriptableObject _plannerEventSo;
        private readonly PlannerBranch _plannerBranches;
        private readonly List<IPlannable> _plannables = new List<IPlannable>();
        private readonly Stack<IPlannable> _planQueue = new Stack<IPlannable>();
        private PlannerState _currentState = PlannerState.PlanStarting;
        public Planner(BaseEventScriptableObject scriptableObject)
        {
            _plannerEventSo = scriptableObject;
            scriptableObject.Subscribe(this);
        }

        public enum PlannerState : int
        {
            PlanFailed = -1,
            PlanStarting = 0,
            PlanInProgress = 1,
            PlanCompleted = 2
        };

        public bool CreatePlanQueue()
        {
            List<IPlannable> branch = _plannerBranches.PlannableBranch;
            List<IPlannable> orderPlans = _plannables.OrderBy(x => x.Order).ToList();
            foreach (var plan in orderPlans)
            {
                _planQueue.Push(plan);
            }

            return true;
        }

        public void CalculatePlan(List<IPlannable> plannables)
        {
            _plannables.Clear();
            _plannables.AddRange(plannables);
        }
        public bool ExecutePlan()
        {
            _currentState = PlannerState.PlanInProgress;
            PlannerConfig config = new PlannerConfig
            {
                PlannerState = _currentState
            };
            while (_planQueue.Count > 0)
            {
                IPlannable plan = _planQueue.Pop();
                config = plan.PlannedExecution(config);
                _currentState = config.PlannerState;
                if (_currentState == PlannerState.PlanFailed)
                    return false;
            }

            _currentState = PlannerState.PlanCompleted;
            return true;
        }

        public void Execute(object obj)
        {
            if (obj.GetType() != typeof(IPlannable)) return;
            IPlannable plan = (IPlannable) obj;
            _plannables.Add(plan);
        }
    }
}