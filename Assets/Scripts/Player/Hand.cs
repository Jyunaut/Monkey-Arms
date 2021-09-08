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

        [SerializeField] private float _grabReleaseDuration;
        private float _grabReleaseTimer;
        private bool _isGrabbing;

        public Rigidbody2D Rigidbody2d { get; private set; }
        public Collider2D Collider2d { get; private set; }

        private Inputs _inputs;
        private WaitForSeconds _disableTime;

        private void Awake()
        {
            _inputs = new Inputs(_hand);
            Rigidbody2d = GetComponent<Rigidbody2D>();
            Collider2d = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (_isGrabbing)
                UpdateGrab();
            else
                UpdateMove();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(GlobalStrings.kHandle))
            {
                transform.position = other.transform.position;
                Rigidbody2d.velocity = Vector2.zero;
                _isGrabbing = true;
            }            
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

        private void UpdateGrab()
        {
            if (_inputs.IsPressingMovement)
            {
                if (_grabReleaseTimer >= _grabReleaseDuration)
                {
                    _isGrabbing = false;
                    _grabReleaseTimer = 0f;
                }
                else
                {
                    _grabReleaseTimer += Time.deltaTime;
                }
            }
            else
            {
                _grabReleaseTimer = 0f;
            }
        }
    }
}