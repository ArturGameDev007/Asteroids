using _Project.Scripts.Services.CloudSave;
using _Project.Scripts.Services.Save;
using _Project.Scripts.UI.StartMenu.SavesViewPanel;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class SaveSynchronizationService : ISaveSynchronization
    {
        private readonly ISaveService _localSaveService;
        private readonly ICloudSaveSample _cloudSaveSample;

        private SaveDataTypePresenter _saveDataTypeTypePresenter;

        public SaveSynchronizationService(ISaveService localSaveService, ICloudSaveSample cloudInitialize)
        {
            _localSaveService = localSaveService;
            _cloudSaveSample = cloudInitialize;
        }

        public async UniTask GetActualSaveData()
        {
            _localSaveService.Load();

            CheckCloudUpdate().Forget();
        }

        public async UniTask SaveToCloud()
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

        public void SetPresenter(SaveDataTypePresenter presenter)
        {
            _saveDataTypeTypePresenter = presenter;
        }

        private async UniTaskVoid CheckCloudUpdate()
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

            Debug.Log(
                $"Local - {localData.BestResult}: {localData.LastSaveTime}, Cloud - {cloudData.BestResult}: {cloudData.LastSaveTime}");

            if (cloudData.LastSaveTime > localData.LastSaveTime)
            {
                Debug.Log("Облачное сохранение новее.");
                _localSaveService.Save(cloudData);
            }
            else if (localData.LastSaveTime > cloudData.LastSaveTime)
            {
                Debug.Log("Локальное сохранение новее.");
                
                var choiceLocal = await _saveDataTypeTypePresenter.WaitUserChoice(localData, cloudData);

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