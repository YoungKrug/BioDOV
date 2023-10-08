using System.Collections.Generic;
using _Scripts.Planning.Interfaces;

namespace _Scripts.Planning
{
    public class PlannerBranch
    {
        public readonly List<IPlannable> PlannableBranch = new List<IPlannable>();

        public void AddToBranch(IPlannable plannable)
        {
            PlannableBranch.Add(plannable);
        }
    }
}