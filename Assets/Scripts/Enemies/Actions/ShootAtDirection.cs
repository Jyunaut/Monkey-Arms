using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Shoot At Direction", menuName = "Enemy Action Library/Shoot At Direction")]
    public class ShootAtDirection : Action
    {
        [field: SerializeField] public RangeAttack rangeAttack { get; set; }
        [field: SerializeField] public Vector3 position { get; set; }

        public override void OnEnter()
        {
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
