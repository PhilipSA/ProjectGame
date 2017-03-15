﻿using Assets.Scripts.Engine;
using Assets.Scripts.Engine.Audio;
using Assets.Scripts.InteractingObjects.Abstraction;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions
{
    public class Ice : AnimatedSprite
    {
        public AudioSource AudioSource;
        public AudioPlayer AudioPlayer;

        void Awake()
        {
            AudioPlayer = new AudioPlayer();
            AudioSource = gameObject.AddComponent<AudioSource>();
            base.Awake("/Textures/Contraptions/Padding");
        }

        void Start()
        {
            AudioSource.clip = Resources.Load<AudioClip>("Audio/InteractingObjectsAudio/ContraptionsAudio/Ice");
        }

        void OnCollisionStay2D(Collision2D col)
        {
            AudioPlayer.PlayOnce(AudioSource);
            GameEngineHelper.GetCurrentGameEngine().Player.PlayerCollider.OnIceCollision(col);
        }
    }
}
