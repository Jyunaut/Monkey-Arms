using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [field: SerializeField] public Vector2 Direction { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        Direction = Direction.normalized;
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() { Move(); }
    private void Move() { _rigidbody2D.velocity = Speed * Direction; }
    public void SetBullet(Vector2 direcion, float speed) { Direction = direcion; Speed = speed; }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col != null)
        {
            Debug.Log("Hit!");
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}
