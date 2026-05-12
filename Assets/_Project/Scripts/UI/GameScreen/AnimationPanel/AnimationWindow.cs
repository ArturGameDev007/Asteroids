using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen.AnamationPanel
{
    public class AnimationWindow : IAnimationWindow
    {
        private RectTransform _panelTransform;
        private Sequence _sequence;

        public void AnimatePanel(RectTransform panel, CanvasGroup canvasGroup, float duration)
        {
            _sequence = DOTween.Sequence();

            Vector2 targetPosition = panel.anchoredPosition;
            Vector2 startShift = targetPosition - new Vector2(0f, 300f);

            canvasGroup.alpha = 0f;
            panel.anchoredPosition = startShift;
            _sequence.SetUpdate(true);

            _sequence.Append(canvasGroup.DOFade(1f, duration));
            _sequence.Join(panel.DOAnchorPos(targetPosition, duration).SetEase(Ease.OutBack));
        }
    }
}