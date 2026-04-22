using System;
using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Save;
using _Project.Scripts.UI.GameScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class LoseManager : IInitializable, IDisposable
    {
        public event Action OnReviveRequested;

        private readonly ISaveService _saveService;
        private readonly IAdsService _adsService;
        private readonly IAdsRewardsType _adsRewardsType;
        private readonly IGameFactory _gameFactory;
        private readonly LoseResourceManager _loseResourceManager;
        private readonly ILoseModel _scoreData;
        private readonly RestartGame _restartGame;

        private LosePresenter _losePresenter;

        public bool IsGameOver { get; private set; }
        private bool _canInternet = Application.internetReachability != NetworkReachability.NotReachable;

        private bool _canRevive;
        private bool _isWaitingForRevive;

        public LoseManager(ISaveService saveService, IAdsService adsService, IAdsRewardsType adsRewardsType, IGameFactory gameFactory,
            LoseResourceManager loseResourceManager,
            ILoseModel scoreData, RestartGame restartGame)
        {
            _saveService = saveService;
            _adsService = adsService;
            _adsRewardsType = adsRewardsType;
            _gameFactory = gameFactory;
            _loseResourceManager = loseResourceManager;
            _scoreData = scoreData;
            _restartGame = restartGame;

            _canRevive = true;
        }

        public void Initialize()
        {
            _adsService.OnAdsFinished += OnAdsClosed;
        }

        public async UniTask HandleGameOver()
        {
            IsGameOver = true;

            if (_canRevive && _canInternet)
            {
                _canRevive = false;
                _isWaitingForRevive = true;
                _adsService.ShowAdsReward(_adsRewardsType.Revive);
            }
            else
            {
                IsGameOver = true;
                _isWaitingForRevive = false;

                bool noAds = _saveService.Load().IsNoAdsPurchased;
                
                if (noAds)
                {
                    Debug.Log("Реклама отключена покупкой NoAds.");
                }
                else if (!_canInternet)
                {
                    Debug.Log("Реклама не показана: отсутствует интернет.");
                }
                else
                {
                    Debug.Log("Показана межстрочная реклама.");
                    _adsService.ShowAdsInterstitial();
                }
                
                await ShowLoseScreen();
            }
        }

        public void Dispose()
        {
            _adsService.OnAdsFinished -= OnAdsClosed;

            if (_losePresenter != null)
            {
                _losePresenter.OnRestartClick -= OnRestartButtonClick;
                _losePresenter?.Dispose();
            }
        }

        private async UniTask ShowLoseScreen()
        {
            var prefab = await _loseResourceManager.LoseScreenLoadAsync();

            _losePresenter = _gameFactory.CreateLoseScreen(prefab);

            if (_losePresenter != null)
            {
                _losePresenter.OnRestartClick += OnRestartButtonClick;
                _losePresenter.Open(_scoreData.Score);
            }
        }

        private void OnRestartButtonClick()
        {
            _losePresenter.Close();
            _restartGame.RestartScene();
        }

        public void Unload()
        {
            _loseResourceManager.Unload();
        }

        private void OnAdsClosed(string adsType)
        {
            if (_isWaitingForRevive && adsType == _adsRewardsType.Revive)
            {
                _isWaitingForRevive = false;
                IsGameOver = false;
                OnReviveRequested?.Invoke();
            }
        }
    }
}