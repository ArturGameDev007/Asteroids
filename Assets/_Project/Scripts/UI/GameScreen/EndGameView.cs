using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class EndGameView : MonoBehaviour
    {
        [field: SerializeField] public LoseView LoseView { get; private set; }
    }
}