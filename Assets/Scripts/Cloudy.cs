using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BUG
{
    public class Cloudy : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                Player player = collision.GetComponent<Player>();
                if (!player.IsUmbrellaOpen)
                {
                    // TODO: LOSE
                    Debug.Log($"LOSE: You hit by Cloudy.");
                }
            }
        }
    }
}