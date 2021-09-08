using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Inputs.Hand _hand;
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float MoveAcceleration { get; private set; }

        public Rigidbody2D Rigidbody2d { get; private set; }
        public Collider2D Collider2d { get; private set; }

        private Inputs _inputs;

        private void Awake()
        {
            _inputs = new Inputs(_hand);
            Rigidbody2d = GetComponent<Rigidbody2D>();
            Collider2d = GetComponent<Collider2D>();
        }

        private void Update()
        {
            UpdateMove();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            
        }

        private void UpdateMove()
        {
            Vector2 currentVelocity = Rigidbody2d.velocity;
            Vector2 direction = Vector2.zero;
            Vector2 targetVelocity = Vector2.zero;

            if (_inputs.IsPressingMovement)
            {
                direction = (Vector2.right * _inputs.Horizontal.Invoke() + Vector2.up * _inputs.Vertical.Invoke()).normalized;
                targetVelocity = MoveSpeed * direction;
            }
            Rigidbody2d.velocity = Vector2.SmoothDamp(currentVelocity, targetVelocity, ref currentVelocity, MoveAcceleration);
        }
    }
}