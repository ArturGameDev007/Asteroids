using System;

namespace _Project.Scripts.Services.Save
{
    public interface ISaveService
    {
        public event Action OnSaved;
        
        public void Save(SaveData saveData);
        
        public SaveData Load();
    }
}