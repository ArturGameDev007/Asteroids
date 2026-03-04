using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseUIComponents : MonoBehaviour
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TextScore { get; private set; }
        [field: SerializeField] public Button RestartButton { get; private set; }
    }
}