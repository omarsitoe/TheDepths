using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public Vector3 offset;
    public float shootSpeed;

    void Start()
    {
        transform.position += offset;
        transform.parent = null;
    }

    void FixedUpdate()
    {
        transform.Translate(-1.0f * shootSpeed,0f, 0f);
    }
}
