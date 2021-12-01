using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour
{
    public int layerToIgnore;

    private void Start()
    {
        IgnoreCollision();
    }

    private void IgnoreCollision()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, layerToIgnore);
    }
}
