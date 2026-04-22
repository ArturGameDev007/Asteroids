using System;
using _Project.Scripts.Services.Save;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI.StartMenu.SavesViewPanel
{
    public class SaveDataTypePresenter : IInitializable, IDisposable, ISaveDataType
    {
        private readonly IConflictView _conflictView;

        private UniTaskCompletionSource<bool> _choiceSave;

        public SaveDataTypePresenter(IConflictView conflictView)
        {
            _conflictView = conflictView;
        }

        public void Initialize()
        {
            _conflictView.OnLocalButtonClick += OnLocalClicked;
            _conflictView.OnCloudButtonClick += OnCloudClicked;
        }

        public void Dispose()
        {
            _conflictView.OnLocalButtonClick -= OnLocalClicked;
            _conflictView.OnCloudButtonClick -= OnCloudClicked;
        }

        public async UniTask<bool> WaitUserChoice(SaveData local, SaveData cloud)
        {
            _conflictView.ShowSavesView(local, cloud);
            _conflictView.SetActive(true);

            _choiceSave = new UniTaskCompletionSource<bool>();

            var result = await _choiceSave.Task;

            _conflictView.SetActive(false);

            return result;
        }

        private void OnLocalClicked()
        {
            if (_choiceSave == null)
                return;

            _choiceSave?.TrySetResult(true);
        }

        private void OnCloudClicked()
        {
            if (_choiceSave == null)
                return;

            _choiceSave?.TrySetResult(false);
        }
    }
}