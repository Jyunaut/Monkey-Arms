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
            _playerBody = (Vector2)GameObject.FindGameObjectWithTag("Monkey Body").transform.position;
            position = Controller.transform.position;
            switch(armTarget)
            {
                case ArmTarget.Left:
                    _playerArm = (Vector2)GameObject.FindGameObjectWithTag("Monkey Left Hand").transform.position;
                    break;
                case ArmTarget.Right:
                    _playerArm = (Vector2)GameObject.FindGameObjectWithTag("Monkey Right Hand").transform.position;
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
            if(rangeAttack != null)
            {
                for(int i = 0; i < rangeAttack.shots; i++)
                {
                    yield return new WaitForSeconds(rangeAttack.fireRate);
                    GameObject bullet = Instantiate(rangeAttack.bullet.gameObject, position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = rangeAttack.speed * rangeAttack.direction;
                }
            }
            Transition();
        }
    }
}
