using Cysharp.Threading.Tasks;

namespace _Project.Scripts.UI.StartMenu
{
    public interface ISceneLoader
    {
        public UniTask LoadSceneAsync();
    }
}