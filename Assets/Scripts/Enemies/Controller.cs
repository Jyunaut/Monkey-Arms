using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        public List<Action> ActionList;
        [SerializeField] private Action currentAction;
        private Action rootAction;
        private Vector2 faceDirection;

        public Rigidbody2D Rigidbody2D { get; set; }

        private void Awake()
        {
            if(ActionList.Count > 0)
            {
                foreach (Action item in ActionList)
                {
                    item.SetAction(this);
                }
                rootAction = ActionList[0];
            }
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            SetAction(rootAction);
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
            SetAction(rootAction);
        }

        public void SetAction(Action newAction)
        {
            currentAction?.OnExit();
            currentAction = newAction;
            currentAction?.OnEnter();
        }
    }
}
    
