using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameplayCompositionRoot  _gameplayCompositionRoot;

        private void Awake()
        {
            _gameplayCompositionRoot.Compose();
        }

        private void Start()
        {
            _gameplayCompositionRoot.Game.Initialize();
        }

        private void OnDestroy()
        {
            _gameplayCompositionRoot.Game.Dispose();
        }
    }
}