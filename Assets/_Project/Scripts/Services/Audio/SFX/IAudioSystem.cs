using UnityEngine;

namespace _Project.Scripts.Services.Audio.SFX
{
    public interface IAudioSystem
    {
        public void PlayExplosionForKillClip(Vector3 position);
        
        public void PlayShootClip(Vector3 position);
    }
}