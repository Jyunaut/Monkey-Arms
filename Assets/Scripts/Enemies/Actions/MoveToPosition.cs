using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Move To Position", menuName = "Enemy Action Library/Move To Position")]
    public class MoveToPosition : Action
    {
        [field: SerializeField] public Vector2 targetPosition { get; set; }
        public float speed;

        public override void OnFixedUpdate()
        {
            if(Vector2.Distance(targetPosition, Controller.transform.position) > 0.1f)
            {
                Vector2 direction = targetPosition - (Vector2)Controller.transform.position;
                Controller.Rigidbody2D.velocity = speed * direction;
            }
            else
            {
                Transition();
            }
        }

        public override void OnExit()
        {
            Controller.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}
