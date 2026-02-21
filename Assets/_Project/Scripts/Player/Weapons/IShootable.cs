namespace _Project.Scripts.Player.Weapons
{
    public interface IShootable
    {
        public bool IsPaused { get; set; }
        
        public void EnableControl();
        public void DisableControl();
    }
}