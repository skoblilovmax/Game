using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 clickPos;
    Vector2 direction;
    public float acceleration;
    Rigidbody2D rb;
    float startTime, endTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            acceleration = Mathf.Clamp01(endTime - startTime) + 1;
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = transform.position - clickPos;
            rb.AddForce(direction.normalized * acceleration, ForceMode2D.Impulse);
            endTime = 0f;
            startTime = 0f;
        }
    }
}
