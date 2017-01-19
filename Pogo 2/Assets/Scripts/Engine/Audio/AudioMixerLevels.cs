using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Engine.Audio
{
    public class AudioMixerLevels : MonoBehaviour
    {
        public AudioMixer masterMixer;

        public void SetSfxLevel(float sfxLvl)
        {
            masterMixer.SetFloat("SoundEffectsVolume", sfxLvl);
        }

        public void SetMusicLevel(float musicLvl)
        {
            masterMixer.SetFloat("BackgroundMusicVolume", musicLvl*10);
        }
    }
}
