using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Shoot At Arm", menuName = "Enemy Action Library/Shoot At Arm")]
    public class ShootAtPlayerArm : Action
    {
        public enum ArmTarget { Left, Right }
        public RangeAttack rangeAttack;
        public Vector3 position;
        public ArmTarget armTarget = ArmTarget.Left;
        private Vector2 playerBody;
        private Vector2 playerArm;

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
            Vector2 InBetween = (playerBody - (Vector2)Controller.transform.position) + (playerArm - (Vector2)Controller.transform.position);
            Vector2 Direction = InBetween - (Vector2)Controller.transform.position;
            rangeAttack.direction = Direction.normalized;
            Controller.StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            yield return new WaitForSeconds(0f);
            if(rangeAttack != null)
            {
                rangeAttack.bullet.SetBullet(rangeAttack.direction, rangeAttack.speed);
                Instantiate(rangeAttack.bullet.gameObject, position, Quaternion.identity);
            }
            IsLastAction = true;
            Transition();
        }
    }
}
