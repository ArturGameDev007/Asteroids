using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services.RemoteConfigs
{
    public interface IRemoteConfigs
    {
        public RemoteConfigsRoot RemoteConfig { get; }
        
        public UniTask Initialize();
    }
}