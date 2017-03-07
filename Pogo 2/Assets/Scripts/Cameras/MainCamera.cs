using Assets.Scripts.Engine;
using Assets.Scripts.InteractingObjects.Player;
using UnityEngine;

namespace Assets.Scripts.Cameras
{
    public class MainCamera : MonoBehaviour {

        public Player Player;       
        public Bounds CameraBounds;
        public Camera Camera;

        void Start()
        {
            Player = GameEngineHelper.GetCurrentGameEngine().Player;
            Camera = GetComponent<Camera>();
            var boundsRect = GameObject.Find("BoundingBox").GetComponent<RectTransform>();
            CameraBounds.center = boundsRect.rect.center;
            CameraBounds.min = boundsRect.rect.min;
            CameraBounds.max = boundsRect.rect.max;
        }

        void LateUpdate()
        {
            float camVertExtent = Camera.orthographicSize;
            float camHorzExtent = Camera.aspect * camVertExtent;

            float leftBound = CameraBounds.min.x + camHorzExtent;
            float rightBound = CameraBounds.max.x - camHorzExtent;
            float bottomBound = CameraBounds.min.y + camVertExtent;
            float topBound = CameraBounds.max.y - camVertExtent;

            float camX = Mathf.Clamp(Player.transform.position.x, leftBound, rightBound);
            float camY = Mathf.Clamp(Player.transform.position.y, bottomBound, topBound);

            Camera.transform.position = new Vector3(camX, camY, Camera.transform.position.z);        
        }
    }
}
