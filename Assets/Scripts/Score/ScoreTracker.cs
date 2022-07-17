using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Score
{
    public class ScoreTracker: MonoBehaviour
    {
        private static ScoreTracker _instance;

        public Dictionary<string, int> StepsPerLevel { get; }= new();

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this);
        }

        public void SetLevelSteps(string level, int steps)
        {
            StepsPerLevel[level] = steps;
        }
    }
}