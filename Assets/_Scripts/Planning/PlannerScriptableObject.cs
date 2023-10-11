using System.Collections.Generic;
using _Scripts.Planning.Interfaces;
using UnityEngine;

namespace _Scripts.Planning
{
    [CreateAssetMenu(fileName = "PlannerData", menuName = "Planning/PlannerScriptableObject", order = 1)]
    public class PlannerScriptableObject : ScriptableObject
    {
        public string objectNamesToPlan;
        private PlannerBranch _branch;
        private List<IPlannable> _plannables = new List<IPlannable>();

        public void Init(List<IPlannable> objectsToPlan)
        {
            IPlannable[] allPlannableObjects = objectsToPlan.ToArray();
            List<IPlannable> plannableObjects = new List<IPlannable>();
            foreach (var obj in allPlannableObjects)
            {
                if (objectNamesToPlan.Contains(obj.GetName()))
                {
                    plannableObjects.Add(obj);
                }
            }
            _plannables = plannableObjects;
            _branch.PlannableBranch.Clear();
            _branch.PlannableBranch.AddRange(plannableObjects);
        }
        
    }
}