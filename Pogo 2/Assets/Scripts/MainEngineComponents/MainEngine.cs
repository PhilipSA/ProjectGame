using Assets.Scripts.Engine.Audio;
using UnityEngine;

namespace Assets.Scripts.MainEngineComponents
{
    public class MainEngine : MonoBehaviour
    {
        public AudioMixerLevels AudioMixerLevels;
        public GraphicsComponent GraphicsComponent;

        void Awake()
        {
            AudioMixerLevels = GetComponentInChildren<AudioMixerLevels>();
            GraphicsComponent = new GraphicsComponent();
        }

        public static MainEngine GetMainEngine
        {
            get
            {
                return (MainEngine)FindObjectOfType(typeof(MainEngine));
            }
        }
    }
}
