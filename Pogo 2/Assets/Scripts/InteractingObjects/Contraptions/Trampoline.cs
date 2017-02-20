using Assets.Scripts.Engine;
using Assets.Scripts.InteractingObjects.Abstraction;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions
{
    public class Trampoline : AnimatedSprite
    {
        public AudioSource AudioSource;

        void Awake()
        {
            base.Awake("/Textures/Contraptions/Trampoline");
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            GameEngineHelper.GetCurrentGameEngine().Player.OnTrampolineCollision();
        }
    }
}
