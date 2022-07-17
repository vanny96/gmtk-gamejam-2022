using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameEnd: MonoBehaviour
    {
        [SerializeField] private string menuLevel;

        private void Start()
        {
            Destroy(AudioManager.Instance.gameObject);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(menuLevel);
        }
    }
}