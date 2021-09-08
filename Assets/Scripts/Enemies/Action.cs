using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using UnityEngine.Events;

namespace Enemy
{
    public abstract class Action : ScriptableObject
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
            public float speed;
            public Bullet bullet;
        }

        [field: SerializeField] public Action TransitionTo { get; set; }
        [field: SerializeField] public Controller Controller { get; set; }
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
            Controller?.SetAction(TransitionTo);
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
