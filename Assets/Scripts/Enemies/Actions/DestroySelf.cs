using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class DestroySelf : Action
    {
        public override void OnEnter()
        {
            Destroy(Controller.gameObject);
        }
    }
}
