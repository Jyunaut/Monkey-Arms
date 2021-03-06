using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Inputs
    {
        public enum Hand { Left, Right }

        public delegate int HorizontalMovement();
        public HorizontalMovement Horizontal;
        public delegate int VerticalMovement();
        public VerticalMovement Vertical;

        public bool IsEnabled { get; set; } = true;
        public bool IsPressingMovement => IsEnabled && (Mathf.Abs(Horizontal.Invoke()) > 0 || Mathf.Abs(Vertical.Invoke()) > 0);
        public bool Interact => IsEnabled && Input.GetKeyDown(KeyCode.Space);

        public Inputs(Hand hand)
        {
            switch (hand)
            {
                case Hand.Left:
                    Horizontal = LHMove;
                    Vertical = LVMove;
                    break;
                case Hand.Right:
                    Horizontal = RHMove;
                    Vertical = RVMove;
                    break;
            }
        }

        private int LHMove()
        {
            if (!IsEnabled)
                return 0;
            if (Input.GetKey(KeyCode.A))
                return -1;
            if (Input.GetKey(KeyCode.D))
                return 1;
            return 0;
        }

        private int LVMove()
        {
            if (!IsEnabled)
                return 0;
            if (Input.GetKey(KeyCode.S))
                return -1;
            if (Input.GetKey(KeyCode.W))
                return 1;
            return 0;
        }

        private int RHMove()
        {
            if (!IsEnabled)
                return 0;
            if (Input.GetKey(KeyCode.J))
                return -1;
            if (Input.GetKey(KeyCode.L))
                return 1;
            return 0;
        }

        private int RVMove()
        {
            if (!IsEnabled)
                return 0;
            if (Input.GetKey(KeyCode.K))
                return -1;
            if (Input.GetKey(KeyCode.I))
                return 1;
            return 0;
        }
    }
}