using UnityEngine;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class PerformanceShipView : MonoBehaviour
    {
        [field: SerializeField] public CoordinateDisplay CoordinateDisplay { get; private set; }
        [field: SerializeField] public ViewCurrentAmountLaser ViewCurrentAmountLaser { get; private set; }
    }
}