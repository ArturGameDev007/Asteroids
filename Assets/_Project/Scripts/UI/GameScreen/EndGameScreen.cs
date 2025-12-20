using System;
using UnityEngine;

namespace Assets.Scripts.UI.GameScreen
{
    public class EndGameScreen : Window
    {
        public event Action OnRestartButtonClick;

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
            OnRestartButtonClick?.Invoke();
        }
    }
}

