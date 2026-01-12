using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class GameTimeController : MonoBehaviour
    {
        public void Initialize()
        {
            StartGame();
        }
        
        private void StartGame()
        {
            Time.timeScale = 1;
        }

        public void LoseGame()
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }
    }
}