using System.Linq;
using Assets.Scripts.Engine.Animation;
using Assets.Scripts.Enums;

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

        public void SetNewDefaultSprite(string spritename)
        {
            SetDefaultSprite(Sprites.First(x => x.name == spritename));
        }

        public void AnimateBlink(string spriteNameOne, string spriteNameTwo, int priority)
        {
            if (priority > Priority)
            {
                SetBlinkSprites(spriteNameOne, spriteNameTwo);
                SetAnimationType(AnimationTypeEnum.Blink);
                Priority = priority;
            }
        }

        public void AnimatePermanent(string spriteName)
        {
            SetNewDefaultSprite(spriteName);
            enabled = false;
        }
    }
}
