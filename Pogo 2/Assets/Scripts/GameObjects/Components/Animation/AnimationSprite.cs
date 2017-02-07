using System.Linq;
using Assets.Scripts.Engine.Animation;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Animation
{
    public class AnimationSprite : AnimationHandler
    {
        protected override void Start()
        {
            base.Start();
        }

        public void SetBlinkSprites(string indexOne, string indexTwo)
        {
            BlinkSprites = Sprites.Where(x => x.name == indexOne || x.name == indexTwo).ToArray();
            Debug.Log(BlinkSprites.Length);
        }
    }
}
