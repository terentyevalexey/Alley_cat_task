using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Scripts
{
    public class CatEnemy : MonoBehaviour
    {
        public float speed = 2f;
        public float xDirection = -1;
        // Start is called before the first frame update

        private void FixedUpdate()
        {
            transform.Translate( speed * xDirection * Time.fixedDeltaTime, 0f, 0f);
            if (transform.position.x is >= 7 or <= -7)
            {
                transform.Rotate(Vector2.up, 180);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                transform.localScale = new Vector3(10f, 10f, 10f);
            }
        }
    }
}