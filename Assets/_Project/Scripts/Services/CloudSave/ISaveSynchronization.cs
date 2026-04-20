using _Project.Scripts.UI.StartMenu.SavesViewPanel;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Infrastructure
{
    public interface ISaveSynchronization
    {
        public UniTask GetActualSaveData();
    
        public UniTask SaveToCloud();
        
        public void SetPresenter(SaveDataTypePresenter presenter);
    }
}