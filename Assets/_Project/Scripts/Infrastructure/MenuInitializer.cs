using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class MenuInitializer : IInitializable
    {
        private readonly ICloudInitialize _cloudInitializer;
        private readonly ISaveSynchronization _synchronizationService;

        public MenuInitializer(ICloudInitialize cloudInitializer, ISaveSynchronization synchronizationService)
        {
            _cloudInitializer = cloudInitializer;
            _synchronizationService = synchronizationService;
        }

        public void Initialize()
        {
            StartSync().Forget();
        }

        private async UniTaskVoid StartSync()
        {
            await _cloudInitializer.InitializeCloud();
            
            await _synchronizationService.GetActualSaveData();
        }
    }
}