﻿using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions.Trebuchet
{
    public class Beam : MonoBehaviour
    {
        public HingeJoint2D HingeJoint2D;

        void Awake()
        {
            HingeJoint2D = GetComponent<HingeJoint2D>();
        }
    }
}