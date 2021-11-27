using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BUG
{
    public class Player : MonoBehaviour
    {
        [Header("STATS")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float jumpForce = 10f;

        [Header("Velocity Limit")]
        [SerializeField] private float maxFallVelocity;
        [SerializeField] private float maxUmbrellaFallVelocity;

        [Header("PROPERTIES")]
        [SerializeField] private LayerMask layerMask;

        private bool isUmbrellaOpen = false;
        private Rigidbody2D playerRb;
        private float inputHorizontal;

        SpriteRenderer render;
        Color defaultColor;

        private void Start()
        {
            render = GetComponent<SpriteRenderer>();
            playerRb = GetComponent<Rigidbody2D>();
            defaultColor = render.color;
        }

        private void Update()
        {
            InputPlayer();
        }

        private void FixedUpdate()
        {
            Walk(inputHorizontal);
            CheckUmbrellaCondition();
            CheckMaxVelocity();
        }

        private void InputPlayer()
        {
            // WALK
            inputHorizontal = Input.GetAxisRaw("Horizontal");
        
            // JUMP
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }

            // OPEN UMBRELLA
            if (Input.GetKeyDown(KeyCode.K))
            {
                ToogleUmbrella();
            }
        }

        private void Walk(float direction)
        {
            // ANIMATE
            if (direction == 1)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (direction == -1)
                GetComponent<SpriteRenderer>().flipX = true;

            // WALK
            playerRb.velocity = new Vector2(direction * moveSpeed, playerRb.velocity.y);
        }

        private void Jump()
        {
            if (!GroundCheck()) return;

            playerRb.AddForce(new Vector2(0f, jumpForce * 200f));
        }

        private void ToogleUmbrella()
        {
            isUmbrellaOpen = !isUmbrellaOpen;
        }

        private void CheckUmbrellaCondition()
        {
            // debug
            if (isUmbrellaOpen)
            {
                render.color = Color.green;
            }
            else
            {
                render.color = defaultColor;
            }

            // code
            if (isUmbrellaOpen)
            {
                if (playerRb.velocity.y <= -maxUmbrellaFallVelocity)
                {
                    playerRb.velocity = new Vector2(playerRb.velocity.x, -maxUmbrellaFallVelocity);
                }
            }
        }

        private void CheckMaxVelocity()
        {

            // fall velocity
            if (playerRb.velocity.y <= -maxFallVelocity)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, -maxFallVelocity);
            }
        }

        private bool GroundCheck()
        {
            float offsetHeight = 0.1f;
            Collider2D collider = GetComponent<Collider2D>();
            RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, offsetHeight, layerMask);

            return raycastHit.collider != null;
        }

        public bool IsUmbrellaOpen
        {
            get { return isUmbrellaOpen; }
        }

        // Hit by rain
        private void OnParticleCollision(GameObject other)
        {
            if (other.tag == "Rain")
            {
                if (!isUmbrellaOpen)
                {
                    Debug.Log($"LOSE: Hit by rain.");
                }
            }
        }
    }
}