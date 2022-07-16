using UnityEngine;
using UnityEngine.SceneManagement;

namespace Goal
{
    public class GoalBehaviour : MonoBehaviour
    {
        [SerializeField] private string nextLevel;

        private void OnTriggerEnter2D(Collider2D col)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
