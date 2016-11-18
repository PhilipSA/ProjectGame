﻿using System;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class ChargeBar : MonoBehaviour {

        public float BarDisplay; //current progress
        public Vector2 Pos = new Vector2(20, 1600);
        public Vector2 Size = new Vector2(10, 20);

        void OnGUI()
        {
            useGUILayout = false;
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.green);
            texture.Apply();
            UnityEngine.GUI.skin.box.normal.background = texture;
            for (float i = PlayerBounceLogic.MinimumBouncePower; i <= Math.Round(BarDisplay, 2); i += PlayerBounceLogic.BouncePowerIncrease)
            {
                Debug.Log(BarDisplay);
                var displayPos = new Vector2(Pos.x, Pos.y * i / 10 + 20);
                UnityEngine.GUI.Box(new Rect(displayPos, Size), GUIContent.none);
            }
        }

        void Update()
        {

        }
    }
}
