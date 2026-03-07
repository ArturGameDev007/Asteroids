using _Project.Scripts.Player.Weapons;
using UnityEngine;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class PerformanceShipView : MonoBehaviour
    {
        [field: SerializeField] public CoordinateDisplay CoordinateDisplay { get; private set; }
        [field: SerializeField] public ViewCurrentAmountLaser ViewCurrentAmountLaser { get; private set; }
        [field: SerializeField] public GenerateLaser GenerateLaser { get; private set; }
    }
}