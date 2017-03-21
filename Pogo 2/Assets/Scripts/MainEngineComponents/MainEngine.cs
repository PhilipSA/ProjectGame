using Engine.Audio;
using UnityEngine;

namespace MainEngineComponents
{
    public class MainEngine : MonoBehaviour
    {
        public AudioMixerLevels AudioMixerLevels;
        public GraphicsComponent GraphicsComponent;

        void Awake()
        {
            AudioMixerLevels = GetComponentInChildren<AudioMixerLevels>();
            GraphicsComponent = new GraphicsComponent();
            Resources.UnloadUnusedAssets();
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
