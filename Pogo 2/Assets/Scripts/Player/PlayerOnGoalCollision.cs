using UnityEngine;
using System.Collections;
using Assets.Engine;

public class PlayerOnGoalCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        GameEngineHelper.GetCurrentGameEngine().Player.OnGoalCollision();
    }
}
