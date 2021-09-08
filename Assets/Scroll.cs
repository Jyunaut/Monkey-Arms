using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Rigidbody2D;

    private void Start() { Rigidbody2D = this.GetComponent<Rigidbody2D>(); }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Speed * Vector2.down;
    }
}
