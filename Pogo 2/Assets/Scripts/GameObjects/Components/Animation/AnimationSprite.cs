using System.Linq;
using Assets.Scripts.Engine.Animation;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Animation
{
    public class AnimationSprite : AnimationHandler
    {
        protected override void Start()
        {
            base.Start();
        }

        public void SetBlinkSprites(string spriteNameOne, string spriteNameTwo)
        {
            BlinkSprites = Sprites.Where(x => x.name == spriteNameOne || x.name == spriteNameTwo).ToArray();
            Debug.Log(BlinkSprites.Length);
        }

        public void AnimateBlink(string spriteNameOne, string spriteNameTwo)
        {
            SetBlinkSprites(spriteNameOne, spriteNameTwo);
            AnimationType = AnimationTypeEnum.Blink;
        }
    }
}
