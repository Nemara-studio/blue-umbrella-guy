using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Vector2[] movePoint;
    public float speed;
    public float delayMove;

    private bool isMoved;
    private int nextPositionIndex = 0;

    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMove());
    }

    private void Update()
    {
        Move();
    }

    public IEnumerator StartMove()
    {
        _ = nextPositionIndex >= movePoint.Length - 1 ? nextPositionIndex = 0 : nextPositionIndex++;

        yield return new WaitForSeconds(delayMove);

        float flipped = transform.position.x > movePoint[nextPositionIndex].x ? 180f : 0f;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, flipped, transform.rotation.eulerAngles.z);

        isMoved = true;
    }

    private void Move()
    {
        if (isMoved)
        {
            anim.SetBool("Run", true);
            transform.position = Vector2.MoveTowards(transform.position, movePoint[nextPositionIndex], speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if ((Vector2) transform.position == movePoint[nextPositionIndex])
        {
            isMoved = false;
            StartCoroutine(StartMove());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.singleton.Lose();
        }
    }
}
