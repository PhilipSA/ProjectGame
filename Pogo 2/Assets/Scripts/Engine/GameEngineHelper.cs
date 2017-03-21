using UnityEngine;

namespace Engine
{
    public static class GameEngineHelper
    {
        public static GameEngine GetCurrentGameEngine()
        {
            return (GameEngine)Object.FindObjectOfType(typeof(GameEngine));
        }
    }
}
