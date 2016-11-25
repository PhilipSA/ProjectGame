using UnityEngine;
using Assets.Engine;

public class PlayerOnGoalCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        GameEngineHelper.GetCurrentGameEngine().GameEvents.OnPlayerGoalCollision();
    }
}
