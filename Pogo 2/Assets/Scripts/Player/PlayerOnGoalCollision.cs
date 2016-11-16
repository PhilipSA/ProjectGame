using UnityEngine;
using System.Collections;

public class PlayerOnGoalCollision : MonoBehaviour {

    private GameObject parent;

    void Start()
    {
        parent = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        parent.SendMessage("OnGoalCollision");
    }
}
