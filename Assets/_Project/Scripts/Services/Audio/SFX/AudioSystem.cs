using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Services.Audio.SFX
{
    public class AudioSystem : IAudioSystem
    {
        private readonly ObjectPool<ExplosionClip> _poolExplosionClip;
        private readonly ObjectPool<ShootClip> _poolShootClip;

        public AudioSystem(ObjectPool<ExplosionClip> poolExplosionClip, ObjectPool<ShootClip> poolShootClip)
        {
            _poolExplosionClip = poolExplosionClip;
            _poolShootClip = poolShootClip;
        }

        public void PlayExplosionForKillClip(Vector3 position)
        {
            PlayClip(_poolExplosionClip, position);
        }

        public void PlayShootClip(Vector3 position)
        {
            PlayClip(_poolShootClip, position);
        }

        private void PlayClip<T>(ObjectPool<T> pool, Vector3 position) where T : MonoBehaviour
        {
            if (pool == null)
                return;

            var audio = pool.GetObject();
            audio.transform.position = position;

            if (audio.TryGetComponent(out AudioSource source))
                source.Play();
        }
    }
}