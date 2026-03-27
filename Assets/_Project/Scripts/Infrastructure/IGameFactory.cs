using _Project.Scripts.Player;
using _Project.Scripts.UI.GameScreen;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        // public UniTask<PlayerController> CreatePlayerAsync();
        
        public UniTask<LosePresenter> CreateLoseScreenAsync();
    }
}