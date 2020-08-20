using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxScrolling : MonoBehaviour
{
    [SerializeField] private Vector2 pMult;

    public Transform camera;
    public Vector3 camPos;

    void Start()
    {
        camera = Camera.main.transform;
        camPos = camera.position;
    }

    void LateUpdate()
    {
        Vector3 dest = camera.position - camPos;
        transform.position += new Vector3(dest.x * pMult.x, dest.y * pMult.y, 0.0f);

        camPos = camera.position;
    }
}
