using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace Score
{
    public class UIScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textArea;
        [SerializeField] private List<LevelSteps> developerSteps;

        private ScoreTracker _scoreTracker;
        private Dictionary<string, int> _developersStepsMap;

        private void Awake()
        {
            _developersStepsMap = developerSteps.ToDictionary(_ => _.level, _ => _.steps);
        }

        void Start()
        {
            _scoreTracker = FindObjectOfType<ScoreTracker>();
            Debug.Log(PrepareText());
            textArea.text = PrepareText();
        }

        private string PrepareText()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"{"Level",-10} {"Steps",-7} {"Developer Steps",-15}\n\n");

            List<(string, int)> levelsSteps = _scoreTracker.GetLevelsSteps();
            for (int i=0; i < levelsSteps.Count; i++)
            {
                string levelName = levelsSteps[i].Item1;
                int playerSteps = levelsSteps[i].Item2;
                int developersSteps = _developersStepsMap[levelName];
                string formattedString = $"{levelName,-10} {playerSteps,-7} {developersSteps, -7}\n";
                builder.Append(formattedString);
            }

            int totalSteps = levelsSteps.Select(_ => _.Item2).Aggregate(0, (sum, value) => sum + value);
            int developerTotal = developerSteps.Select(_ => _.steps).Aggregate(0, (sum, value) => sum + value);
            builder.Append($"\n{"Total",-10} {totalSteps,-7} {developerTotal, -7}");

            return builder.ToString();
        }
        
        [Serializable]
        public struct LevelSteps
        {
            public string level;
            public int steps;
        }
    }
}