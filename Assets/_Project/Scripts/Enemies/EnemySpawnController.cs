namespace _Project.Scripts.Enemies
{
    public class EnemySpawnController
    {
        private readonly GeneratorEnemies[] _generators;

        public EnemySpawnController(GeneratorEnemies[] generators)
        {
            _generators = generators;
        }

        public void StartAll()
        {
            foreach (var gen in _generators) 
                gen.StartSpawning();
        }

        public void StopAndClearAll()
        {
            foreach (var gen in _generators)
            {
                gen.StopSpawning();
                gen.StopAllEnemies();
            }
        }
    }
}