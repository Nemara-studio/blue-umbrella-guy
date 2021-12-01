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
        [SerializeField] private Animator anim;
        [SerializeField] private Collider2D umbrellaCollider;
        [SerializeField] private LayerMask layerMask;

        [Header("SFX")]
        public AudioSource walkSound;
        public AudioSource jumpSound;
        public AudioSource rainOnUmbrellaSound;
        public AudioSource openUmbrellaSound;

        private bool isUmbrellaOpen = false;
        private Rigidbody2D playerRb;
        private float inputHorizontal;

        private void Start()
        {
            playerRb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (GameManager.singleton.isPaused) return;

            InputPlayer();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            if (GameManager.singleton.isPaused) return;

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
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));
            else if (direction == -1)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z));

            // WALK
            playerRb.velocity = new Vector2(direction * moveSpeed, playerRb.velocity.y);

            // sound
            if (direction != 0 && GroundCheck() && !walkSound.isPlaying)
            {
                Debug.Log($"{walkSound.isPlaying}");
                walkSound.Play();
            }
            else if (direction == 0 || !GroundCheck())
            {
                walkSound.Stop();
            }
        }

        private void Jump()
        {
            if (!GroundCheck()) return;

            playerRb.AddForce(new Vector2(0f, jumpForce * 200f));
        }

        private void ToogleUmbrella()
        {
            isUmbrellaOpen = !isUmbrellaOpen;
            if (isUmbrellaOpen)
            {
                umbrellaCollider.enabled = true;
                openUmbrellaSound.Play();
            }
            else
            {
                umbrellaCollider.enabled = false;
            }

            // animation
            anim.SetBool("Umbrella", isUmbrellaOpen);
        }

        private void CheckUmbrellaCondition()
        {
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

        private void UpdateAnimation()
        {
            anim.SetBool("Run", (Input.GetAxisRaw("Horizontal") != 0) && GroundCheck());
        }

        public bool IsUmbrellaOpen
        {
            get { return isUmbrellaOpen; }
        }

        // Hit by rain
        private void OnParticleCollision(GameObject other)
        {
            if (GameManager.singleton.isPaused) return;

            if (other.tag == "Rain")
            {

                if (!isUmbrellaOpen)
                {
                    Debug.Log($"LOSE: Hit by rain.");
                    GameManager.singleton.Lose();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Win Area")
            {
                GameManager.singleton.Win();
            }

            if (collision.tag == "Rain")
            {
                rainOnUmbrellaSound.Play();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Rain")
            {
                rainOnUmbrellaSound.Stop();
            }
        }
    }
}