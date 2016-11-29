﻿using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects
{
    public class GoalArea : MonoBehaviour
    {
        private AudioSource _audioSource;

        void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (enabled)
            {
                AudioHandler.PlayAudio(_audioSource);
                GameEngineHelper.GetCurrentGameEngine().GameEvents.OnPlayerGoalCollision();
                this.enabled = false;
            }
        }
    }
}
