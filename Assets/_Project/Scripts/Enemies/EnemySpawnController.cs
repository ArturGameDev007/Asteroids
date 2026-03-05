namespace _Project.Scripts.Enemies
{
    public class EnemySpawnController
    {
        private readonly GeneratorEnemies[] _generators;

        public EnemySpawnController(GeneratorEnemies[] generators)
        {
            _generators = generators;
        }

        public void Process(float  deltaTime)
        {
            foreach (var generator in _generators)
                generator.Process(deltaTime);
        }

        public void StartAll()
        {
            foreach (var generator in _generators) 
                generator.StartSpawning();
        }

        public void StopAndClearAll()
        {
            foreach (var generator in _generators)
            {
                generator.StopSpawning();
                generator.StopAllEnemies();
            }
        }
    }
}