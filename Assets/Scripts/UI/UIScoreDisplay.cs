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
            builder.Append($"{"Level",-15} {"Steps",-7} {"Developer Steps",-15}\n\n");

            Dictionary<string, int> playerLevelStaps = _scoreTracker.StepsPerLevel;
            foreach (LevelSteps developerStep in developerSteps)
            {
                string levelName = developerStep.level;
                if (playerLevelStaps.TryGetValue(levelName, out int playerSteps))
                {
                    int developersSteps = _developersStepsMap[levelName];
                    string formattedString = $"{levelName,-15} {playerSteps,-7} {developersSteps, -7}\n";
                    builder.Append(formattedString);
                }
            }

            int totalSteps = playerLevelStaps.Select(_ => _.Value).Aggregate(0, (sum, value) => sum + value);
            int developerTotal = developerSteps.Where(_ => playerLevelStaps.ContainsKey(_.level))
                .Select(_ => _.steps)
                .Aggregate(0, (sum, value) => sum + value);
            
            builder.Append($"\n{"Total",-15} {totalSteps,-7} {developerTotal, -7}");
            
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