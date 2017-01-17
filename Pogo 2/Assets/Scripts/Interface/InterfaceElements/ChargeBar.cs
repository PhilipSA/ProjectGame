using System;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class ChargeBar : MonoBehaviour {

        public float BarDisplay; //current progress
        public Vector2 Pos = new Vector2(20, 500);
        public Vector2 Size = new Vector2(10, 20);

        void OnGui()
        {
            useGUILayout = false;
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.green);
            texture.Apply();
            UnityEngine.GUI.skin.box.normal.background = texture;
            for (float i = PlayerBounceLogic.MinimumBouncePower; i <= Math.Round(BarDisplay, 2); i += PlayerBounceLogic.BouncePowerIncrease)
            {
                float posY = Pos.y * i / 10;
                var displayPos = new Vector2(Pos.x, Pos.y - posY);
                UnityEngine.GUI.Box(new Rect(displayPos, Size), GUIContent.none);
            }
        }

        void Update()
        {

        }
    }
}
