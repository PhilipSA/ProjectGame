using System.Linq;
using Assets.Scripts.Engine.Animation;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Components.Animation
{
    public class SpriteAnimation : AnimationHandler
    {
        protected override void Start()
        {
            base.Start();
        }

        public void SetBlinkSprites(string spriteNameOne, string spriteNameTwo)
        {
            BlinkSprites = Sprites.Where(x => x.name == spriteNameOne || x.name == spriteNameTwo).ToArray();
        }

        public void AnimateBlink(string spriteNameOne, string spriteNameTwo)
        {
            SetBlinkSprites(spriteNameOne, spriteNameTwo);
            AnimationType = AnimationTypeEnum.Blink;
        }
    }
}
