using Assets.Scripts.Engine.Audio;
using UnityEngine;

namespace Assets.Scripts.MainEngineComponents
{
    public class MainEngine : MonoBehaviour
    {
        public static AudioMixerLevels AudioMixerLevels;

        void Start()
        {
            AudioMixerLevels = GetComponentInChildren<AudioMixerLevels>();
        }
    }
}
