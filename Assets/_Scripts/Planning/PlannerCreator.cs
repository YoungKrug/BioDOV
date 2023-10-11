using System;
using System.Collections.Generic;
using _Scripts.Planning.Interfaces;
using UnityEngine;

namespace _Scripts.Planning
{
    public class PlannerCreator : MonoBehaviour
    {
        public PlannerScriptableObject PlannerScriptableObject;
        public List<GameObject> ObjectsToPlan = new List<GameObject>();
        private void Awake()
        {
            List<IPlannable> plannables = new List<IPlannable>();
            foreach (var obj in ObjectsToPlan)
            {
                foreach (var component in obj.GetComponents(typeof(Component)))
                {
                    //component.
                    if (component.GetType() == typeof(IPlannable))
                    {
                        plannables.Add((IPlannable)component);
                    }

                    object t = component;
                    if(t.GetType() == typeof(IPlannable))
                        Debug.Log(t.GetType());
                }
            }
            PlannerScriptableObject.Init(plannables);
        }
    }
}