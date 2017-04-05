using Engine;
using UnityEngine;

namespace InteractingObjects
{
    public class PlayerSpawn : MonoBehaviour
    {
        void Start()
        {
            GameEngineHelper.GetCurrentGameEngine().Player.transform.position = this.transform.position;
        }
    }
}
