using UnityEngine;
using UnityEngine.UI;

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

    protected abstract void OnButtonCLick();

    public abstract void Open();
    public abstract void Close();
}
