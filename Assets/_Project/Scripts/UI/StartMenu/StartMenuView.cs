using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button BuyProducts { get; private set; }
    }
}