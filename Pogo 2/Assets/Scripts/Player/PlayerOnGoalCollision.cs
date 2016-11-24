using UnityEngine;
using System.Collections;

public class PlayerOnGoalCollision : MonoBehaviour {

    private GameObject parent;
    public bool HasTriggered = false;

    void Start()
    {
        parent = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(!HasTriggered) parent.SendMessage("OnGoalCollision");
        HasTriggered = true;
    }
}
