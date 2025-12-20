using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.GameScreen
{
    public abstract class Window : MonoBehaviour
    {
        [field: SerializeField] public Canvas PanelCanvas { get; private set; }
        [field: SerializeField] public Button ActionButton { get; private set; }

        private void Awake()
        {
            Initialize(ActionButton);
        }

        public void Initialize(Button button)
        {
            ActionButton = GetComponentInChildren<Button>();
            ActionButton.onClick.AddListener(OnButtonCLick);
        }

        public void Cleanup()
        {
            ActionButton.onClick.RemoveListener(OnButtonCLick);
        }

        public abstract void Open();

        public abstract void Close();

        protected abstract void OnButtonCLick();
    }
}
