using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}