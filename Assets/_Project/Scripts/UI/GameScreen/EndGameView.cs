using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class EndGameView : MonoBehaviour
    {
        [field: SerializeField] public LoseUIComponents LoseViewUIComponents { get; private set; }
    }
}