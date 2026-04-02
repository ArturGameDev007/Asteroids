using System;
using UnityEngine;

namespace _Project.Scripts.Services.Save
{
    [Serializable]
    public class SaveData
    {
        [field: SerializeField] public int BestResult { get; private set; }

        public void UpdateBestResult(int newValue)
        {
            if (newValue > BestResult)
            {
                BestResult = newValue;
            }
        }
    }
}