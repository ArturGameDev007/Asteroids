using _Project.Scripts.Services.Save;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.UI.StartMenu.SavesViewPanel
{
    public interface ISaveDataType
    {
        public UniTask<bool> WaitUserChoice(SaveData local, SaveData cloud);
    }
}