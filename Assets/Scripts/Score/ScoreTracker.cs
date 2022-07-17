using System;
using System.Collections.Generic;
using UnityEngine;

namespace Score
{
    public class ScoreTracker: MonoBehaviour
    {
        private static ScoreTracker _instance;

        private List<(string, int)> _stepsPerLevel = new();

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
            _stepsPerLevel.Add((level,steps));
        }

        public List<(string, int)> GetLevelsSteps()
        {
            return _stepsPerLevel;
        }
    }
}