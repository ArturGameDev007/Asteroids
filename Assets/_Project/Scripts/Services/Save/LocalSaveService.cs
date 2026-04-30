using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

namespace _Project.Scripts.Services.Save
{
    public class LocalSaveService : ISaveService
    {
        private const string BEST_SCORE_DATA = "BestScore";

        public event Action OnSaved;

        public UniTask Save(SaveData saveData)
        {
            string json = JsonConvert.SerializeObject(saveData);

            PlayerPrefs.SetString(BEST_SCORE_DATA, json);
            PlayerPrefs.Save();
            
            OnSaved?.Invoke();
            return UniTask.CompletedTask;
        }

        public UniTask<SaveData> Load()
        {
            if (!PlayerPrefs.HasKey(BEST_SCORE_DATA))
            {
                var resultData = new SaveData();
                return UniTask.FromResult(resultData);
            }
            
            string json = PlayerPrefs.GetString(BEST_SCORE_DATA);

            return UniTask.FromResult(JsonConvert.DeserializeObject<SaveData>(json));
            // return JsonConvert.DeserializeObject<SaveData>(json);
        }
    }
}