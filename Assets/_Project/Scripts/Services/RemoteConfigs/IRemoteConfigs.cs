using System.Threading.Tasks;

namespace _Project.Scripts.Services.RemoteConfigs
{
    public interface IRemoteConfigs
    {
        public RemoteConfigsData RemoteConfig { get; }
        
        public Task Initialize();
    }
}