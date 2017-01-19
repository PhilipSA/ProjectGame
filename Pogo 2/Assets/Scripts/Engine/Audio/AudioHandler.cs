using UnityEngine;

namespace Assets.Scripts.Engine.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public static class AudioHandler
    {
        public static void PlayAudio(AudioSource audioSource)
        {
            audioSource.Play();
        }
    }
}
