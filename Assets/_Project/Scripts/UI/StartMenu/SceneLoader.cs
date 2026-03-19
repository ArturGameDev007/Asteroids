using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI.StartMenu
{
    public class SceneLoader : ISceneLoader
    {
        private const string GAME_SCENE_NAME = "Gameplay";
        
        public void LoadScene()
        {
            SceneManager.LoadScene(GAME_SCENE_NAME);
        }
    }
}