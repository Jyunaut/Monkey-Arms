using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Shoot At Direction", menuName = "Enemy Action Library/Shoot At Direction")]
    public class ShootAtDirection : Action
    {
        public RangeAttack rangeAttack;
        public Vector3 position;

        public override void OnEnter()
        {
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
