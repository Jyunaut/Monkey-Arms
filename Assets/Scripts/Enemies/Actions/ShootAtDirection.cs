using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ShootAtDirection : Action
    {
        [field: SerializeField] public RangeAttack rangeAttack { get; set; }
        [field: SerializeField] public Vector3 position { get; set; }

        public override void OnEnter()
        {
            Controller.StartCoroutine(Shoot());
            position = Controller.transform.position;
            rangeAttack.bullet.SetBullet(rangeAttack.direction.normalized, rangeAttack.speed);
        }

        IEnumerator Shoot()
        {
            if(rangeAttack != null)
            {
                for(int i = 0; i < rangeAttack.shots; i++)
                {
                    yield return new WaitForSeconds(rangeAttack.fireRate);
                    Instantiate(rangeAttack.bullet.gameObject, position, Quaternion.identity);
                }
            }
            Transition();
        }
    }
}
