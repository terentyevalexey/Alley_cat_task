using UnityEngine;

namespace Scripts
{
    public class Cat : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float jumpHeight = 12f;
        public float speed = 2f;
        public float xDirection;
        public float yDirection;
        public Animator an;
        public bool facingRight;
        private bool _onRoad;
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        // Start is called before the first frame update

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            an = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            xDirection = Input.GetAxis("Horizontal");
            yDirection = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            // calculating turn 
            switch (xDirection)
            {
                case < 0 when facingRight:
                    transform.Rotate(Vector2.up, 180);
                    facingRight = !facingRight;
                    break;
                case > 0 when !facingRight:
                    transform.Rotate(Vector2.up, 180);
                    facingRight = !facingRight;
                    break;
            }
            // movement anim
            an.SetBool(IsWalking, xDirection != 0);

            // calculating movement
            rb.velocity = new Vector2(speed * xDirection, rb.velocity.y);
            if (rb.velocity.y != 0f) return;
            // calculating jump and fall
            if (yDirection > 0f)
            {
                an.SetBool(IsJumping,  true);
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
            else if (yDirection < 0f && !_onRoad)
            {
                an.SetBool(IsJumping, true);
                transform.Translate(0f, -0.1f, 0f);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                an.SetBool(IsJumping, false);
                _onRoad = other.gameObject.name.Equals("Road");
            }
        }
    }
}