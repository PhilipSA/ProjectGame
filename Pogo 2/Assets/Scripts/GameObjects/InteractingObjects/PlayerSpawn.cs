using Engine;
using UnityEngine;

namespace GameObjects.InteractingObjects
{
    public class PlayerSpawn : MonoBehaviour
    {
        void Start()
        {
            GameEngineHelper.GetCurrentGameEngine().Player.transform.position = this.transform.position;
        }
    }
}
