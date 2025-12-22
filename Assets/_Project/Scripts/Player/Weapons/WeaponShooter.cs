using UnityEngine;

namespace Assets._Project.Scripts.Player.Weapons
{
    public class WeaponShooter : MonoBehaviour
    {
        public void CreateShoot(GameObject weapon, Transform spawnPoint)
        {
            Instantiate(weapon, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
