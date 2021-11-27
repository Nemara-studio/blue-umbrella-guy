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

        isMoved = true;
    }

    private void Move()
    {
        if (isMoved)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoint[nextPositionIndex], speed * Time.deltaTime);
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
            Debug.Log($"LOSE: get hit by enemy.");
        }
    }
}
