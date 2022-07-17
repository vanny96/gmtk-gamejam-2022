using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameEnd: MonoBehaviour
    {
        [SerializeField] private string menuLevel;

        public void BackToMenu()
        {
            SceneManager.LoadScene(menuLevel);
        }
    }
}