using System;
using UnityEngine;

public class EndGameScreen : Window
{
    public event Action RestartButtonClick;

    private float _stopTimeGame = 0f;
    private float _startTimeGame = 1f;

    public override void Open()
    {
        Time.timeScale = _stopTimeGame;

        PanelCanvas.gameObject.SetActive(true);
        ActionButton.interactable = true;
    }

    public override void Close()
    {
        Time.timeScale = _startTimeGame;

        PanelCanvas.gameObject.SetActive(false);
        ActionButton.interactable = false;
    }

    protected override void OnButtonCLick()
    {
        RestartButtonClick?.Invoke();
    }
}
