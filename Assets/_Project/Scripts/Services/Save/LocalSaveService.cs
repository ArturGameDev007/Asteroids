using UnityEngine;

namespace _Project.Scripts.Services.Save
{
    public class LocalSaveService : ISaveService
    {
        private const string BEST_SCORE_DATA = "BestScore";

        public void Save(SaveData saveData)
        {
            string json = JsonUtility.ToJson(saveData);

            PlayerPrefs.SetString(BEST_SCORE_DATA, json);
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            int minValue = 0;
            
            if (!PlayerPrefs.HasKey(BEST_SCORE_DATA))
            {
                var resultData = new SaveData();
                resultData.BestResult = minValue;
                return resultData;
            }
            
            string json = PlayerPrefs.GetString(BEST_SCORE_DATA);

            return JsonUtility.FromJson<SaveData>(json);
        }
    }
}