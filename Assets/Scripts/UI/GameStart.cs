using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameStart: MonoBehaviour
    {
        [SerializeField] private string firstLevel;

        public void StartGame()
        {
            SceneManager.LoadScene(firstLevel);
        }
    }
}