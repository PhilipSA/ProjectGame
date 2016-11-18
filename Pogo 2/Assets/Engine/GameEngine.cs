using Assets.Scripts.GUI;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Engine
{
    public class GameEngine : MonoBehaviour
    {
        public GUIHandler GuiHandler;
        public Player Player;

        void Start()
        {
            GuiHandler = gameObject.AddComponent<GUIHandler>();
            Player = GameObject.Find("Player").AddComponent(typeof(Player)) as Player;
        }

        void Update()
        {
            
        }

        public void Victory()
        {
            GuiHandler.DisplayVictoryScreen();
        }

    }
}
