using System;
using UnityEngine;

namespace _Scripts.LevelCreation
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    public class LevelDataScriptableObject : ScriptableObject
    {
        [SerializeField]
        public LevelConfig levelConfig;
    }
}