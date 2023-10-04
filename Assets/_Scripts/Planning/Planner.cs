using System.Collections.Generic;
using System.Linq;
using _Scripts.Planning.Interfaces;

namespace _Scripts.Planning
{
    // THe items involved in the action
    // The Precondictions, facts that must be true for the action to run
    // And how it effects the world after completing the interaction
    public class Planner
    {
        private readonly List<IPlannable> _plannables = new List<IPlannable>();
        private readonly Stack<IPlannable> _planQueue = new Stack<IPlannable>();
        public PlannerState CurrentState = PlannerState.PlanStarting;

        public enum PlannerState : int
        {
            PlanFailed = -1,
            PlanStarting = 0,
            PlanInProgress = 1,
            PlanCompleted = 2
        };

        public bool CreatePlanQueue()
        {
            List<IPlannable> orderPlans = _plannables.OrderBy(x => x.Order).ToList();
            foreach (var plan in orderPlans)
            {
                _planQueue.Push(plan);
            }

            return true;
        }

        public void CalculatePlan()
        {
            
        }
        public bool ExecutePlan()
        {
            CurrentState = PlannerState.PlanInProgress;
            PlannerConfig config = new PlannerConfig
            {
                PlannerState = CurrentState
            };
            while (_planQueue.Count > 0)
            {
                IPlannable plan = _planQueue.Pop();
                config = plan.PlannedExecution(config);
                CurrentState = config.PlannerState;
                if (CurrentState == PlannerState.PlanFailed)
                    return false;
            }

            CurrentState = PlannerState.PlanCompleted;
            return true;
        }

    }
}