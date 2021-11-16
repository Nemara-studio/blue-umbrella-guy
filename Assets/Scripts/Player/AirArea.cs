using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BUG
{
    public class AirArea : MonoBehaviour
    {
        [SerializeField] private float airForce;
        [SerializeField] private Vector2 airDirection;

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                Player player = collision.GetComponent<Player>();
                if (player.IsUmbrellaOpen)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(airDirection * airForce * Time.fixedDeltaTime * 10f);
                }
            }
        }
    }
}