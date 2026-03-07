using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private Game _game;

        [Inject]
        public void Construct(Game game)
        {
            _game = game;
        }

        private void Start()
        {
           _game.Initialize();
        }

        private void Update()
        {
            _game.Tick();
        }

        private void OnDestroy()
        {
            _game.Dispose();
        }
    }
}