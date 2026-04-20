using System.Threading.Tasks;
using _Project.Scripts.Services.Save;

namespace _Project.Scripts.Services.CloudSave
{
    public interface ICloudSaveSample
    {
        public Task Save(SaveData saveData);

        public Task<SaveData> Load();
    }
}