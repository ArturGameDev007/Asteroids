using UnityEngine;

namespace _Project.Scripts.Configs.Player
{
    [CreateAssetMenu(fileName = "NewPlayerController", menuName = "Configs/PlayerController/StaticData", order = 51)]
    public class PlayerControllerConfig : ScriptableObject
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float ForceInput { get; private set; }
    }
}