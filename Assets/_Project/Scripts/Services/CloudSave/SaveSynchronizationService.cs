using System;
using _Project.Scripts.Services.CloudSave;
using _Project.Scripts.Services.Save;
using _Project.Scripts.UI.StartMenu.SavesViewPanel;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class SaveSynchronizationService : ISaveSynchronization, IInitializable, IDisposable
    {
        private readonly ISaveService _localSaveService;
        private readonly ICloudSaveSample _cloudSaveSample;

        private ISaveDataType _saveDataTypePresenter;

        public SaveSynchronizationService(ISaveService localSaveService, ICloudSaveSample cloudInitialize)
        {
            _localSaveService = localSaveService;
            _cloudSaveSample = cloudInitialize;
        }

        public async UniTask GetActualSaveData(ISaveDataType presenter)
        {
            _localSaveService.Load();

            CheckCloudUpdate(presenter).Forget();
        }

        public void Initialize()
        {
            _localSaveService.OnSaved += AutoSaveToCloud;
        }

        public void Dispose()
        {
            _localSaveService.OnSaved -= AutoSaveToCloud;
        }

        private void AutoSaveToCloud()
        {
            SaveToCloud().Forget();
        }

        private async UniTask SaveToCloud()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return;

            var localData = _localSaveService.Load();

            if (localData != null)
            {
                await _cloudSaveSample.Save(localData);
                Debug.Log("Данные сихронизированные с облаком.");
            }
        }

        private async UniTaskVoid CheckCloudUpdate(ISaveDataType presenter)
        {
            var localData = _localSaveService.Load();

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                _localSaveService.Load();
                return;
            }

            var cloudData = await _cloudSaveSample.Load();

            if (cloudData == null)
            {
                if (localData != null)
                    await _cloudSaveSample.Save(localData);

                return;
            }

            if (cloudData.LastSaveTime > localData.LastSaveTime)
            {
                Debug.Log("Облачное сохранение новее.");
                _localSaveService.Save(cloudData);
            }
            else if (localData.LastSaveTime > cloudData.LastSaveTime)
            {
                Debug.Log("Локальное сохранение новее.");

                var choiceLocal = await presenter.WaitUserChoice(localData, cloudData);

                SaveData saveSelect = choiceLocal ? localData : cloudData;

                _localSaveService.Save(saveSelect);
                await _cloudSaveSample.Save(saveSelect);
            }
            else
            {
                Debug.Log("Время сохранений совпадает.");
            }
        }
    }
}