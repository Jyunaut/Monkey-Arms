using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [field: SerializeField] public Vector2 Direction { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float ActiveTime { get; set; } = 5f;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        Direction = Direction.normalized;
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        StartCoroutine(ActiveTimer());
    }

    private void FixedUpdate() { Move(); }
    private void Move() { _rigidbody2D.velocity = Speed * Direction; }
    public void SetBullet(Vector2 direcion, float speed) { Direction = direcion; Speed = speed; }
    
    private IEnumerator ActiveTimer()
    {
        yield return new WaitForSeconds(ActiveTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (LayerMask.GetMask(GlobalStrings.kMonkeyHand) == (LayerMask.GetMask(GlobalStrings.kMonkeyHand) | (1 << col.gameObject.layer)))
        {
            if(col.tag == "Monkey Left Hand")
                Debug.Log("Shot Left hand");
            if(col.tag == "Monkey Right Hand")
                Debug.Log("Shot Right hand");
            _rigidbody2D.velocity = Vector2.zero;
        }
        if (LayerMask.GetMask(GlobalStrings.kMonkeyBody) == (LayerMask.GetMask(GlobalStrings.kMonkeyBody) | (1 << col.gameObject.layer)))
        {
            Debug.Log("Shot Body - insta deth");
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}
