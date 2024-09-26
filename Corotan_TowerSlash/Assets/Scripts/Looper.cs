using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    public float scrollSpeed = 1000f;
    private float height;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        transform.position += Vector3.down * scrollSpeed;

        if (transform.position.y <= startPosition.y - height)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
        }
    }
}
