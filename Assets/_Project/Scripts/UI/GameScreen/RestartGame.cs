using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI.GameScreen
{
    public class RestartGame : MonoBehaviour
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}