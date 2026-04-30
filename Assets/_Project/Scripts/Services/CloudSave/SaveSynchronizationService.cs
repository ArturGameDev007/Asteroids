using System;
using _Project.Scripts.Services.Save;
using _Project.Scripts.UI.StartMenu.SavesViewPanel;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class SaveSynchronizationService : ISaveSynchronization, IInitializable, IDisposable
    {
        private readonly ISaveService _localSave;
        private readonly ISaveService _cloudSave;

        private ISaveDataType _saveDataTypePresenter;

        public SaveSynchronizationService(ISaveService localSave, [Inject(Id = "Cloud")] ISaveService cloudSave)
        {
            _localSave = localSave;
            _cloudSave = cloudSave;
        }

        public async UniTask GetActualSaveData(ISaveDataType presenter)
        {
            await _localSave.Load();

            CheckCloudUpdate(presenter).Forget();
        }

        public void Initialize()
        {
            _localSave.OnSaved += AutoSaveToCloud;
        }
        
        public void Dispose()
        {
            _localSave.OnSaved -= AutoSaveToCloud;
        }

        private void AutoSaveToCloud()
        {
            SaveToCloud().Forget();
        }

        private async UniTask SaveToCloud()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return;

            var localData = await _localSave.Load();

            if (localData != null)
            {
                await _cloudSave.Save(localData);
                Debug.Log("Данные сихронизированные с облаком.");
            }
        }

        private async UniTaskVoid CheckCloudUpdate(ISaveDataType presenter)
        {
            var localData = await _localSave.Load();
            var cloudData = await _cloudSave.Load();

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _localSave.Load();
                return;
            }

            if (cloudData == null)
            {
                if (localData != null)
                    await _cloudSave.Save(localData);

                return;
            }
            
            if (localData.BestResult == cloudData.BestResult)
            {
                Debug.Log("Данные идентичны.");
                return;
            }
            
            if (cloudData.LastSaveTime > localData.LastSaveTime)
            {
                Debug.Log("Облачное сохранение новее.");
                _localSave.Save(cloudData);
            }
            else if (localData.LastSaveTime > cloudData.LastSaveTime)
            {
                Debug.Log("Локальное сохранение новее.");

                var choiceLocal = await presenter.WaitUserChoice(localData, cloudData);

                SaveData saveSelect = choiceLocal ? localData : cloudData;

                _localSave.Save(saveSelect);
                await _cloudSave.Save(saveSelect);
            }
            else
            {
                Debug.Log("Время сохранений совпадает.");
            }
        }
    }
}