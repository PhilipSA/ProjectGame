using GameObjects.Components.Animation;
using UnityEngine;

namespace InteractingObjects.Abstraction
{
    public abstract class AnimatedSprite : MonoBehaviour
    {
        protected SpriteAnimation SpriteAnimation;

        protected virtual void Awake(string spriteTextureLocation)
        {
            SpriteAnimation = gameObject.AddComponent<SpriteAnimation>();
            SpriteAnimation.Location = spriteTextureLocation;
        }
    }
}
