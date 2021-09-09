using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        [field: SerializeField] public List<Action> ActionList { get; set; }
        [SerializeField] private Action currentAction;
        private Action _rootAction;
        private Vector2 _faceDirection;

        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }

        private void Awake()
        {
            if(ActionList.Count > 0)
            {
                foreach (Action item in ActionList)
                {
                    item.SetAction(this);
                }
                _rootAction = ActionList[0];
            }
            Animator = GetComponent<Animator>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            SetAction(_rootAction);
        }

        private void Update()
        {
            currentAction?.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentAction?.OnFixedUpdate();
        }

        // Enemy has completed all their transitions
        public void TriggerActionsComplete()
        {
            // Reset to first move in the list
            SetAction(_rootAction);
        }

        public void SetAction(Action newAction)
        {
            currentAction?.OnExit();
            currentAction = newAction;
            currentAction?.OnEnter();
        }
    }
}
    
