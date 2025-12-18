using System;
using UnityEngine;

public class EndGameScreen : Window
{
    public event Action RestartButtonClick;

    public override void Open()
    {
        Time.timeScale = 0f;

        PanelCanvas.gameObject.SetActive(true);
        ActionButton.interactable = true;
    }

    public override void Close()
    {
        Time.timeScale = 1f;

        PanelCanvas.gameObject.SetActive(false);
        ActionButton.interactable = false;
    }

    protected override void OnButtonCLick()
    {
        RestartButtonClick?.Invoke();
    }
}
