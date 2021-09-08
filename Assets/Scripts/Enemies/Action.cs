using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using UnityEngine.Events;

namespace Enemy
{
    public abstract class Action : MonoBehaviour
    {
        [System.Serializable]
        public class MeleeAttack
        {
            public AnimationClip animationClip;
            public Rect HitBox;
            public int SpawnFrame;

        }
        [System.Serializable]
        public class RangeAttack
        {
            public Vector2 direction;
            public Bullet bullet;
            public float speed;
            public float fireRate;
            public int shots;
        }

        [field: SerializeField] public Action TransitionTo { get; set; }
        [field: SerializeField] public Controller Controller { get; set; }
        [field: SerializeField] public bool IsLastAction { get; set; }
        [field: SerializeField] public float EndDelay { get; set; }
        protected List<Coroutine> coroutines;

        public virtual void SetAction(Controller controller)
        {
            Controller = controller;
            coroutines = new List<Coroutine>();
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        protected virtual void Transition()
        {
            coroutines.Add(Controller.StartCoroutine(DelayAction()));
            IEnumerator DelayAction()
            {
                yield return new WaitForSeconds(EndDelay);
                if (IsLastAction)
                    Controller.TriggerActionsComplete();
                else
                    Controller.SetAction(TransitionTo);
            }
        }
        public virtual void OnExit()
        {
            if (coroutines != null)
                foreach (Coroutine item in coroutines)
                {
                    Controller.StopCoroutine(item);
                }
            coroutines = new List<Coroutine>();
        }
    }
}
