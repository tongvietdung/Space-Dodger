using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPooledObject
{
    public float moveSpeed = 20f;
    public Rigidbody2D rb;

    public void OnObjectSpawn(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed * Time.deltaTime;
    }
}
