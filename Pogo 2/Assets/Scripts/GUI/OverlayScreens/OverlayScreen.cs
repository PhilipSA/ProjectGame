﻿using UnityEngine;

namespace Assets.Scripts.GUI.OverlayScreens
{
    public abstract class OverlayScreen : MonoBehaviour
    {
        public bool IsVisible { get; private set; }
        protected GameObject _canvas;

        protected abstract void Start();

        public virtual void SetVisibility(bool visible)
        {
            IsVisible = visible;
            _canvas.SetActive(visible);
        }
    }
}