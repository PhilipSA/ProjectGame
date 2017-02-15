using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Engine.Animation
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

        private void Blink()
        {
            //Keep track of the time that has passed
            DeltaTime += Time.deltaTime;

            if (DeltaTime >= FrameSeconds)
            {
                DeltaTime -= FrameSeconds;
                SpriteRenderer.sprite = BlinkSprites.First();
                AnimationType = AnimationTypeEnum.None;
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
