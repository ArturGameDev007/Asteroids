namespace _Project.Scripts.Services.Save
{
    public interface ISaveService
    {
        public void Save(SaveData saveData);
        
        public SaveData Load();
    }
}