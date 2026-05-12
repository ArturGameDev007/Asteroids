using UnityEngine;

namespace _Project.Scripts.UI.GameScreen.AnamationPanel
{
    public interface IAnimationWindow
    {
        public void AnimatePanel(RectTransform panel, CanvasGroup canvasGroup, float duration);
    }
}