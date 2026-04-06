namespace _Project.Scripts.Services.RemoteConfigs
{
    public interface IRemoteConfigData
    {
        public float ForceInputShip { get; }
        public float RotationSpeedShip { get; }
        public float SpeedShoot { get; }
        public float LifeTimeShoot { get; }
        public float ReloadTimeLaser { get; }
        public int MaxAmountLaser { get; }
        
        public float EnemySpeed { get; }
        public float RotationSpeed { get; }
        public float SpawnOffset { get; }
        public float Delay { get; }
        public int ScoreForKill { get; }
        
        public int AsteroidPoolSize { get; }
        public int UfoPoolSize { get; }
        
        public int BulletPoolSize { get; }
        public int LaserPoolSize { get; }
    }
}