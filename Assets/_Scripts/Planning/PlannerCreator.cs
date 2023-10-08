using System;
using System.Collections.Generic;
using _Scripts.Planning.Interfaces;
using UnityEngine;

namespace _Scripts.Planning
{
    public class PlannerCreator : MonoBehaviour
    {
        public PlannerScriptableObject PlannerScriptableObject;
        private void Awake()
        {
            PlannerScriptableObject.Init();
        }
    }
}