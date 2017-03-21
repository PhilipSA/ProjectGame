using System.Linq;
using Enums.Animation;
using UnityEngine;

namespace Engine.Animation
{
    //Attach to a sprite that has animations
    public class AnimationHandler : MonoBehaviour
    {
        public AnimationTypeEnum AnimationType;
        public float FrameSeconds = 1;
        //The file location of the sprites within the resources folder
        public string Location;
        public Sprite[] BlinkSprites;
        protected SpriteRenderer SpriteRenderer;
        protected Sprite[] Sprites;
        protected int Frame = 0;
        protected float DeltaTime = 0;
        protected int Priority = 0;
        protected Sprite DefaultSprite;

        // Use this for initialization
        protected virtual void Start()
        {
            AnimationType = AnimationTypeEnum.None;
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Sprites = Resources.LoadAll<Sprite>(Location);
        }

        protected virtual void Update()
        {
            if (AnimationType == AnimationTypeEnum.Loop) Loop();
            if (AnimationType == AnimationTypeEnum.Iterate) Iterate();
            if (AnimationType == AnimationTypeEnum.Blink) Blink();
        }

        protected void SetDefaultSprite(Sprite defaultSprite)
        {
            DefaultSprite = defaultSprite;
            SpriteRenderer.sprite = defaultSprite;
        }

        protected virtual void SetAnimationType(AnimationTypeEnum animationTypeEnum)
        {
            if (enabled) AnimationType = animationTypeEnum;
        }

        private void Blink()
        {
            //Keep track of the time that has passed
            DeltaTime += Time.deltaTime;

            if (DeltaTime >= FrameSeconds)
            {
                DeltaTime -= FrameSeconds;
                SpriteRenderer.sprite = BlinkSprites.First();
                AnimationType = AnimationTypeEnum.None;
                Priority = 0;
                return;
            }
            //Animate sprite with selected frame
            SpriteRenderer.sprite = BlinkSprites.Last();
        }

        // Update is called once per frame
        void Loop()
        {
            //Keep track of the time that has passed
            DeltaTime += Time.deltaTime;

            /*Loop to allow for multiple sprite frame 
             jumps in a single update call if needed
             Useful if frameSeconds is very small*/
            while (DeltaTime >= FrameSeconds)
            {
                DeltaTime -= FrameSeconds;
                Frame++;
                Frame %= Sprites.Length;
            }
            //Animate sprite with selected frame
            SpriteRenderer.sprite = Sprites[Frame];
        }

        void Iterate()
        {
            //Keep track of the time that has passed
            DeltaTime += Time.deltaTime;

            /*Loop to allow for multiple sprite frame 
             jumps in a single update call if needed
             Useful if frameSeconds is very small*/
            while (DeltaTime >= FrameSeconds)
            {
                DeltaTime -= FrameSeconds;
                Frame++;
                //Max limit
                if (Frame >= Sprites.Length)
                    Frame = Sprites.Length - 1;
            }
            //Animate sprite with selected frame
            SpriteRenderer.sprite = Sprites[Frame];
        }
    }
}
