using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ShootAtPlayerArm : Action
    {
        public enum ArmTarget { Left, Right }
        [field: SerializeField] public RangeAttack rangeAttack { get; set; }
        [field: SerializeField] public Vector3 position { get; set; }
        [field: SerializeField] public ArmTarget armTarget { get; set; } = ArmTarget.Left;
        private Vector2 _playerBody;
        private Vector2 _playerArm;

        public override void OnEnter()
        {
            switch(armTarget)
            {
                case ArmTarget.Left:
                //TODO: Get playerArm target from tag
                    break;
                case ArmTarget.Right:
                //TODO: Get playerArm target from tag
                    break;
                default:
                    goto case ArmTarget.Right;
            }
            Vector2 InBetween = (_playerBody - (Vector2)Controller.transform.position) + (_playerArm - (Vector2)Controller.transform.position);
            Vector2 Direction = InBetween - (Vector2)Controller.transform.position;
            rangeAttack.direction = Direction.normalized;
            Controller.StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            yield return new WaitForSeconds(0f);
            if(rangeAttack != null)
            {
                rangeAttack.bullet.SetBullet(rangeAttack.direction.normalized, rangeAttack.speed);
                Instantiate(rangeAttack.bullet.gameObject, position, Quaternion.identity);
            }
            IsLastAction = true;
            Transition();
        }
    }
}
