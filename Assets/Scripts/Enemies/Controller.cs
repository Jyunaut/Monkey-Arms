using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Controller : MonoBehaviour
    {
        public List<Action> ActionList;
        private Action currentAction;
        private Vector2 faceDirection;

        private void Awake()
        {
            if(ActionList.Count > 0)
            {
                foreach (Action item in ActionList)
                {
                    item.SetAction(this);
                }
                currentAction = ActionList[0];
            }
        }

        private void Start()
        {
            SetAction(currentAction);
        }

        private void Update()
        {
            currentAction?.OnUpdate();
        }

        private void FixedUpdate()
        {
            currentAction?.OnFixedUpdate();
        }

        public void SetAction(Action newAction)
        {
            currentAction?.OnExit();
            currentAction = newAction;
            currentAction?.OnEnter();
        }
    }
}
    
