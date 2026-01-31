using System;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseView : MonoBehaviour
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
        
        public void Construct(Button button)
        {
            RestartButton = button;
        }

        // public void ShowPanel()
        // {
        //     // _canvas.gameObject.SetActive(true);
        //     //
        //     if (RestartButton != null)
        //         RestartButton.interactable = true;
        // }
        //
        // public void HidePanel()
        // {
        //     // _canvas.gameObject.SetActive(false);
        //     //
        //     if (RestartButton != null)
        //         RestartButton.interactable = false;
        // }
    }
}