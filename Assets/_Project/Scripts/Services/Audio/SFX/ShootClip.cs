using _Project.Scripts.Enemies;
using _Project.Scripts.Player.Weapons;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Audio.SFX
{
    [RequireComponent(typeof(AudioSource))]
    public class ShootClip : TimedPoolObject
    {
        private AudioSource _audioSource;
        
        private IObjectReturner<ShootClip> _pool;

        [Inject]
        public void Construct(IObjectReturner<ShootClip> pool)
        {
            _pool = pool;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _audioSource.volume = 0.3f;
        }

        protected override void ReturnToPool()
        {
            _pool?.ReturnPool(this);
        }
    }
}