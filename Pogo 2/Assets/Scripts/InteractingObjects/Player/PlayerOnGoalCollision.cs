using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Player
{
    public class PlayerOnGoalCollision : MonoBehaviour
    {
        void OnCollisionEnter2D(Collision2D col)
        {
            GameEngineHelper.GetCurrentGameEngine().GameEvents.OnPlayerGoalCollision();
        }
    }
}
