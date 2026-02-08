using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class EndGameView : MonoBehaviour
    {
        [field: SerializeField] public LoseViewModel LoseViewModel { get; private set; }
        [field: SerializeField] public LoseView LoseView { get; private set; }
    }
}