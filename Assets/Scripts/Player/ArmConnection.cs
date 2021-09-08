using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ArmConnection : MonoBehaviour
    {
        [System.Serializable]
        public class Node
        {
            public Transform transform;
            public bool locked;
            [System.NonSerialized] public Vector2 prevPosition;
        }

        private class Limb
        {
            public Limb(Node nodeA, Node nodeB, float length)
            {
                this.nodeA = nodeA;
                this.nodeB = nodeB;
                this.length = length;
            }
            public Node nodeA, nodeB;
            public float length;
        }
        [SerializeField] private float _armLength;
        [SerializeField] private List<Node> _nodes = new List<Node>();
        private List<Limb> _limbs = new List<Limb>();

        private void Awake()
        {
            for (int i = 0; i < _nodes.Count-1; i++)
            {
                _limbs.Add(new Limb(_nodes[i], _nodes[i+1], _armLength));
            }
        }

        private void FixedUpdate()
        {
            UpdateJointPositions();
        }

        public Node GetHandNode(Inputs.Hand hand)
        {
            switch (hand)
            {
                case Inputs.Hand.Left:
                    return _limbs[0].nodeA;
                case Inputs.Hand.Right:
                    return _limbs[_limbs.Count-1].nodeB;
            }
            Debug.LogWarning("No applicable hand found.", this);
            return null;
        }

        private void UpdateJointPositions()
        {
            foreach (Node n in _nodes)
            {
                if (!n.locked)
                {
                    Vector2 temp = n.transform.position;
                    n.transform.position = (Vector2)n.transform.position + ((Vector2)n.transform.position - n.prevPosition);
                    n.transform.position = (Vector2)n.transform.position + Vector2.up * Physics2D.gravity * Time.deltaTime * Time.deltaTime;
                    n.prevPosition = temp;
                }
            }

            for (int i = 0; i < 50; i++)
            {
                foreach (Limb l in _limbs)
                {
                    Vector2 center = (l.nodeA.transform.position + l.nodeB.transform.position) / 2f;
                    Vector2 direction = (l.nodeA.transform.position - l.nodeB.transform.position).normalized;
                    if (!l.nodeA.locked)
                        l.nodeA.transform.position = center + l.length * direction / 2f;
                    if (!l.nodeB.locked)
                        l.nodeB.transform.position = center - l.length * direction / 2f;
                }
            }
        }
    }
}
