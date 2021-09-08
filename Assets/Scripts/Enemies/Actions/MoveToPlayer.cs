using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class MoveToPlayer : Action
    {
        public enum Target { Body, LeftArm, RightArm }
        [field: SerializeField] public float StopDistance { get; set; } = 1f;
        [field: SerializeField] public Target target { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        private Vector2 _target;
        private bool _isDone = false;

        public override void OnEnter()
        {
            switch (target)
            {
                case Target.Body:
                    // TODO: Get body target tag
                    break;
                case Target.LeftArm:
                    // TODO: Get lArm target tag
                    break;
                case Target.RightArm:
                    // TODO: Get rArm target tag
                    break;
                default:
                    goto case Target.Body;
            }
        }

        public override void OnFixedUpdate()
        {
            if (Vector2.Distance(_target, Controller.transform.position) > StopDistance)
            {
                Vector2 direction = _target - (Vector2)Controller.transform.position;
                Controller.Rigidbody2D.velocity = Speed * direction.normalized;
            }
            else
            {
                if (!_isDone) { Transition(); _isDone = true; }
            }
        }

        public override void OnExit()
        {
            Controller.Rigidbody2D.velocity = Vector2.zero;
            _isDone = false;
        }
    }
}
