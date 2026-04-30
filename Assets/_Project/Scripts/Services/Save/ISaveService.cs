using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.Save
{
    public interface ISaveService
    {
        public event Action OnSaved;
        
        // public void Save(SaveData saveData);
        //
        // public SaveData Load();
        
        public UniTask Save(SaveData saveData);
        
        public UniTask<SaveData> Load();
    }
}