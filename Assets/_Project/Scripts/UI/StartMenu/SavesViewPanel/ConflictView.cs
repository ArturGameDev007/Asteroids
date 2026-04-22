using System;
using _Project.Scripts.Services.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.StartMenu.SavesViewPanel
{
    [RequireComponent(typeof(Canvas))]
    public class ConflictView : MonoBehaviour, IConflictView
    {
        public event Action OnLocalButtonClick;
        public event Action OnCloudButtonClick;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _localInfoText;
        [SerializeField] private TextMeshProUGUI _cloudInfoText;
        
        [Header("Buttons")]
        [SerializeField] private Button _localButton;
        [SerializeField] private Button _cloudButton;

        private Canvas _canvas;
        
        [Inject]
        public void Construct()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            _localButton.onClick.AddListener(OnLocalClicked);
            _cloudButton.onClick.AddListener(OnCloudClicked);
        }

        private void OnDestroy()
        {
            _localButton.onClick.RemoveListener(OnLocalClicked);
            _cloudButton.onClick.RemoveListener(OnCloudClicked);
        }

        public void ShowSavesView(SaveData local, SaveData cloud)
        {
            _localInfoText.text = FormatSaveInfo("Local", local);
            _cloudInfoText.text = FormatSaveInfo("Cloud", cloud);
        }

        private string FormatSaveInfo(string label, SaveData saveData)
        {
            if (saveData == null)
                return $"{label}: No data.";

            DateTime date = new DateTime(saveData.LastSaveTime).ToLocalTime();

            var text = $"<b>Best {label} Result: {saveData.BestResult}</b>\n{date:dd/MM/yyyy, HH:mm}";

            return text;
        }

        public void SetActive(bool active)
        {
            if (_canvas == null)
                return;
            
            _canvas.gameObject.SetActive(active);
        }

        private void OnLocalClicked()
        {
            OnLocalButtonClick?.Invoke();
        }

        private void OnCloudClicked()
        {
            OnCloudButtonClick?.Invoke();
        }
    }
}