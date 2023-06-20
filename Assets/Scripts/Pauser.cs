using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 storedVelocity;
    private float storedAngularVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        storedVelocity = rb.velocity;
        storedAngularVelocity = rb.angularVelocity;
    }

    public void Pause()
    {
        if (rb.isKinematic) return;
        storedVelocity = rb.velocity;
        storedAngularVelocity = rb.angularVelocity;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0.0f;
    }

    public void Resume()
    {
        if (!rb.isKinematic) return;
        rb.isKinematic = false;
        rb.velocity = storedVelocity;
        rb.angularVelocity = storedAngularVelocity;
    }
}
