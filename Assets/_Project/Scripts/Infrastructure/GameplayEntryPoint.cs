using System;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : IInitializable, ITickable, IDisposable
    {
        private readonly Game _game;

        public GameplayEntryPoint(Game game)
        {
            _game = game;
        }

        public void Initialize()
        {
            _game.Initialize();
        }

        public void Tick()
        {
            _game.Tick();
        }

        public void Dispose()
        {
            _game.Dispose();
        }
    }
}