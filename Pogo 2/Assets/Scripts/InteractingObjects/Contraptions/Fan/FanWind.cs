using UnityEngine;

namespace Assets.Scripts.InteractingObjects.Contraptions.Fan
{
    public class FanWind : MonoBehaviour
    {
        public void Update()
        {
            this.transform.Rotate(22, 1, 0);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(col.gameObject);
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 0), ForceMode2D.Force);
        }

        void OnTriggerStay2D(Collider2D col)
        {
            Debug.Log(col.gameObject);
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 0), ForceMode2D.Force);
        }
    }
}
