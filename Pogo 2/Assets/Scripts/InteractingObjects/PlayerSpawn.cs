using Assets.Scripts.Engine;
using UnityEngine;

namespace Assets.Scripts.InteractingObjects
{
    public class PlayerSpawn : MonoBehaviour
    {
        void Start()
        {
            GameEngineHelper.GetCurrentGameEngine().Player.transform.position = this.transform.position;
        }
    }
}
