using UnityEngine;

namespace Assets.Scripts.Interface.InterfaceElements
{
    public class HealthBar : MonoBehaviour {

        public float BarDisplay; //current progress
        public Vector2 Pos = new Vector2(200, 20);
        public Vector2 Size = new Vector2(20, 25);

        void OnGUI()
        {
            useGUILayout = false;
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.red);
            texture.Apply();
            UnityEngine.GUI.skin.box.normal.background = texture;
            for (float i = 0; i < BarDisplay; i++)
            {
                var displayPos = new Vector2(Pos.x + i, Pos.y);
                UnityEngine.GUI.Box(new Rect(displayPos, Size), GUIContent.none);
            }
        }

        void Update()
        {

        }
    }
}
