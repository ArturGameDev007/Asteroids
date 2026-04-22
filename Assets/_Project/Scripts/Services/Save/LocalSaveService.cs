using System;
using UnityEngine;
using Newtonsoft.Json;

namespace _Project.Scripts.Services.Save
{
    public class LocalSaveService : ISaveService
    {
        private const string BEST_SCORE_DATA = "BestScore";

        public event Action OnSaved;

        public void Save(SaveData saveData)
        {
            string json = JsonConvert.SerializeObject(saveData);

            PlayerPrefs.SetString(BEST_SCORE_DATA, json);
            PlayerPrefs.Save();
            
            OnSaved?.Invoke();
        }

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(BEST_SCORE_DATA))
            {
                var resultData = new SaveData();
                return resultData;
            }
            
            string json = PlayerPrefs.GetString(BEST_SCORE_DATA);

            return JsonConvert.DeserializeObject<SaveData>(json);
        }
    }
}