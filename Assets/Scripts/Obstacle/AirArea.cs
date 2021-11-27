using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BUG
{
    public class AirArea : MonoBehaviour
    {
        [SerializeField] private float airForce;
        private Vector3 direction;

        private void Update()
        {
            direction = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward) * Vector3.up;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                Player player = collision.GetComponent<Player>();
                if (player.IsUmbrellaOpen)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(direction * airForce * Time.fixedDeltaTime * 20f);
                }
            }
        }
    }
}