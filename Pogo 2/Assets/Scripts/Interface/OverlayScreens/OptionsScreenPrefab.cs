namespace Assets.Scripts.Interface.OverlayScreens
{
    public class OptionsScreenPrefab : OverlayScreen
    {
        public GraphicOptionsScreen GraphicOptionsScreen;
        public AudioOptionsScreen AudioOptionsScreen;
        public ControlsOptionsScreen ControlsOptionsScreen;
        public GameOptionsScreen GameOptionsScreen;
        // Use this for initialization
        void Awake()
        {
            GraphicOptionsScreen = (GraphicOptionsScreen)GetComponentInChildren(typeof(GraphicOptionsScreen), true);
            AudioOptionsScreen = (AudioOptionsScreen)GetComponentInChildren(typeof(AudioOptionsScreen), true);
            ControlsOptionsScreen = (ControlsOptionsScreen)GetComponentInChildren(typeof(ControlsOptionsScreen), true);
            GameOptionsScreen = (GameOptionsScreen)GetComponentInChildren(typeof(GameOptionsScreen), true);
        }
    }
}
