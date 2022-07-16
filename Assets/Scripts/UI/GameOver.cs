using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOver: MonoBehaviour
    {
        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}