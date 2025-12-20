using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.GameScreen
{
    public abstract class Window : MonoBehaviour
    {
        [field: SerializeField] public GameObject PanelCanvas { get; private set; }
        [field: SerializeField] public Button ActionButton { get; private set; }

        private void OnEnable()
        {
            ActionButton.onClick.AddListener(OnButtonCLick);

        }

        private void OnDisable()
        {
            ActionButton.onClick.RemoveListener(OnButtonCLick);
        }

        //private void Enable()
        //{
        //    ActionButton.onClick.AddListener(OnButtonCLick);
        //}

        //private void Disable()
        //{
        //    ActionButton.onClick.RemoveListener(OnButtonCLick);
        //}

        public abstract void Open();

        public abstract void Close();

        protected abstract void OnButtonCLick();
    }
}
