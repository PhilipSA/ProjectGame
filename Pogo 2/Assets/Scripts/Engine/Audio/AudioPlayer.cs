using UnityEngine;

namespace Engine.Audio
{
    public class AudioPlayer
    {
        public void PlayOnce(AudioSource audioSource)
        {
            if (!audioSource.isPlaying)
            {
                AudioHandler.PlayAudio(audioSource);
            }
        }
    }
}
