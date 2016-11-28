using UnityEngine;

namespace Assets.Scripts.Engine
{
    public static class GameEngineHelper
    {
        public static GameEngine GetCurrentGameEngine()
        {
            return (GameEngine)Object.FindObjectOfType(typeof(GameEngine));
        }
    }
}
