using UnityEngine;

namespace Scripts.Weapons
{
    public class WeaponShooter : MonoBehaviour
    {
        public void CreateShoot(GameObject weapon, Transform spawnPoint)
        {
            Instantiate(weapon, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
