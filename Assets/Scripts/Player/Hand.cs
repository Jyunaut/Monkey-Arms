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
        [SerializeField] private Hand _otherHand;
        private float _grabReleaseTimer;
        [SerializeField] private bool _isGrabbing;
        private GameObject _grabbedHandle;
        private bool _locked;

        public Rigidbody2D Rigidbody2d { get; private set; }
        public Collider2D Collider2d { get; private set; }

        private ArmConnection _armConnection;
        private Inputs _inputs;
        private WaitForSeconds _disableTime;
        private float _prevGravityScale;

        private void Awake()
        {
            Rigidbody2d = GetComponent<Rigidbody2D>();
            Collider2d = GetComponent<Collider2D>();
            _armConnection = GetComponentInParent<ArmConnection>();
            _inputs = new Inputs(_hand);
        }

        private void Update()
        {
            if (_isGrabbing)
                UpdateGrab();
            else if (_otherHand._isGrabbing)
                UpdateMove();

            if (!_inputs.IsPressingMovement)
            {
                _locked = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
                transform.GetChild(0).gameObject.SetActive(true);

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_inputs.IsPressingMovement && !_isGrabbing && other.gameObject.layer == LayerMask.NameToLayer(GlobalStrings.kHandle))
            {
                _grabbedHandle = other.gameObject;
                transform.position = _grabbedHandle.transform.position;
                Rigidbody2d.velocity = Vector2.zero;
                _prevGravityScale = Rigidbody2d.gravityScale;
                Rigidbody2d.gravityScale = 0f;
                _isGrabbing = true;
                _locked = true;
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
            _armConnection.GetHandNode(_hand).locked = true;
            _armConnection.UpdateArmSprites();
            if (_inputs.IsPressingMovement && !_locked)
            {
                if (_grabReleaseTimer >= _grabReleaseDuration)
                {
                    _armConnection.GetHandNode(_hand).locked = false;
                    _isGrabbing = false;
                    _grabReleaseTimer = 0f;
                    _grabbedHandle = null;
                    Rigidbody2d.gravityScale = _prevGravityScale;
                }
                else
                {
                    _grabReleaseTimer += Time.deltaTime;
                }
            }
            else
            {
                _grabReleaseTimer = 0f;
                if (_grabbedHandle != null) transform.position = _grabbedHandle.transform.position;
                else
                {
                    _armConnection.GetHandNode(_hand).locked = false;
                    _isGrabbing = false;
                    _grabReleaseTimer = 0f;
                    _grabbedHandle = null;
                    Rigidbody2d.gravityScale = _prevGravityScale;
                }
            }
        }
    }
}