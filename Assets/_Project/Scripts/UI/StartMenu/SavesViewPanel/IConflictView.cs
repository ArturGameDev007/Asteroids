using System;
using _Project.Scripts.Services.Save;

namespace _Project.Scripts.UI.StartMenu.SavesViewPanel
{
    public interface IConflictView
    {
        public event Action OnLocalButtonClick;
        public event Action OnCloudButtonClick;
        
        public void ShowSavesView(SaveData local, SaveData cloud);
        
        public void SetActive(bool active);
    }
}