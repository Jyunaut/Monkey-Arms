using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Move To Position", menuName = "Enemy Action Library/Move To Position")]
    public class MoveToPosition : Action
    {
        [field: SerializeField] public Vector2 TargetPosition { get; set; }
        [field: SerializeField] public float Speed { get; set; }
        private bool _isDone = false;

        public override void OnFixedUpdate()
        {
            if(Vector2.Distance(TargetPosition, Controller.transform.position) > 0.1f)
            {
                Vector2 direction = TargetPosition - (Vector2)Controller.transform.position;
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
