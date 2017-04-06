using UnityEngine;

namespace GameObjects.InteractingObjects.Contraptions.Fan
{
    public class FanWind : MonoBehaviour
    {
        public void Update()
        {
            this.transform.Rotate(22, 1, 0);
        }

        //void OnTriggerEnter2D(Collider2D col)
        //{
        //    Debug.Log(col.gameObject);
        //    col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-200, 0), ForceMode2D.Force);
        //}

        void OnTriggerStay2D(Collider2D col)
        {
            var direction = col.gameObject.transform.position - transform.position;
            var distance = (col.gameObject.transform.position - transform.position).magnitude;
            direction.Normalize();
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * (300 - distance), ForceMode2D.Force);
        }
    }
}
