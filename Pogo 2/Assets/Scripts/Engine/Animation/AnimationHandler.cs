using UnityEngine;

namespace Assets.Scripts.Engine.Animation
{
    //Attach to a sprite that has animations
    public class AnimationHandler : MonoBehaviour
    {
        public bool Loop;
        public float FrameSeconds = 1;
        //The file location of the sprites within the resources folder
        public string Location;
        private SpriteRenderer _spriteRenderer;
        private Sprite[] _sprites;
        private int _frame = 0;
        private float _deltaTime = 0;

        // Use this for initialization
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sprites = Resources.LoadAll<Sprite>(Location);
        }

        // Update is called once per frame
        void Update()
        {
            //Keep track of the time that has passed
            _deltaTime += Time.deltaTime;

            /*Loop to allow for multiple sprite frame 
             jumps in a single update call if needed
             Useful if frameSeconds is very small*/
            while (_deltaTime >= FrameSeconds)
            {
                _deltaTime -= FrameSeconds;
                _frame++;
                if (Loop)
                    _frame %= _sprites.Length;
                //Max limit
                else if (_frame >= _sprites.Length)
                    _frame = _sprites.Length - 1;
            }
            //Animate sprite with selected frame
            _spriteRenderer.sprite = _sprites[_frame];
        }
    }
}
