using System;
using Newtonsoft.Json;

namespace _Project.Scripts.Services.Save
{
    [Serializable]
    public class SaveData
    {
        [JsonProperty]
        public int BestResult { get; private set; }

        public bool IsNoAdsPurchased;
        public long LastSaveTime;

        public void UpdateBestResult(int newValue)
        {
            if (newValue > BestResult)
            {
                BestResult = newValue;
                LastSaveTime = DateTime.UtcNow.Ticks;
            }
        }
    }
}