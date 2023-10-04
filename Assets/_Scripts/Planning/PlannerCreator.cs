using System.Collections.Generic;
using _Scripts.Planning.Interfaces;

namespace _Scripts.Planning
{
    public class PlannerCreator
    {
        public List<IPlannable> Planner = new List<IPlannable>();
    }
}