using UnityEngine;

namespace _Project.Scripts.Services.Audio.Background
{
    public class MusicBackground : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void Start()
        {
            PlayBackgroundMusic();
        }

        public void PlayBackgroundMusic()
        {
            if (_audioSource != null)
            {
                _audioSource.loop = true;
                _audioSource.volume = 0.3f;
                _audioSource.Play();
            }
        }

        public void StopBackgroundMusic()
        {
            _audioSource.Stop();
        }
    }
}