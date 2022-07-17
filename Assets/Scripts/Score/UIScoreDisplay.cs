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
        [SerializeField] private List<int> developerSteps;

        private ScoreTracker _scoreTracker;

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
                string formattedString = $"{levelsSteps[i].Item1,-10} {levelsSteps[i].Item2,-7} {developerSteps[i], -7}\n";
                builder.Append(formattedString);
            }

            int totalSteps = levelsSteps.Select(_ => _.Item2).Aggregate(0, (sum, value) => sum + value);
            int developerTotal = developerSteps.Aggregate(0, (sum, value) => sum + value);
            builder.Append($"\n{"Total",-10} {totalSteps,-7} {developerTotal, -7}");

            return builder.ToString();
        }
    }
}