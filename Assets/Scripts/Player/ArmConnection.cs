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
            public Node nodeA, nodeB;
            public float length;
            public LineRenderer armSprite;
            public Limb(Node nodeA, Node nodeB, float length)
            {
                this.nodeA = nodeA;
                this.nodeB = nodeB;
                this.length = length;
            }
        }
        [SerializeField] private float _armLength;
        [SerializeField] private float _armWidth;
        [SerializeField] private Material _armMaterial;
        [SerializeField] private List<Node> _nodes = new List<Node>();
        private List<Limb> _limbs = new List<Limb>();

        private void Awake()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                _nodes[i].prevPosition = _nodes[i].transform.position;
                if (i < _nodes.Count-1)
                {
                    _limbs.Add(new Limb(_nodes[i], _nodes[i+1], _armLength));
                    GameObject limb = new GameObject("Arm", typeof(LineRenderer));
                    limb.transform.SetParent(transform, false);
                    _limbs[i].armSprite = limb.GetComponent<LineRenderer>();
                    LineRenderer arm = _limbs[i].armSprite;
                    arm.startWidth = _armWidth;
                    arm.endWidth = _armWidth;
                    arm.material = _armMaterial;
                    arm.sortingLayerName = "Middleground/Front";
                    arm.sortingOrder = -1;
                    arm.positionCount = 2;
                    arm.SetPosition(0, _nodes[i].transform.position);
                    arm.SetPosition(1, _nodes[i+1].transform.position);
                    arm.textureMode = LineTextureMode.Stretch;
                }
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

        public void UpdateArmSprites()
        {
            foreach (Limb l in _limbs)
            {
                l.armSprite.SetPosition(0, l.nodeA.transform.position);
                l.armSprite.SetPosition(1, l.nodeB.transform.position);
            }
        }

        private void UpdateJointPositions()
        {
            foreach (Node n in _nodes)
            {
                if (!n.locked)
                {
                    Vector2 temp = n.transform.position;
                    n.transform.position += n.transform.position - (Vector3)n.prevPosition;
                    // n.transform.position += (Vector3)(Vector2.up * Physics2D.gravity * Time.deltaTime * Time.deltaTime);
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
                    {
                        l.nodeA.transform.position = center + l.length * direction / 2f;
                        l.armSprite.SetPosition(0, l.nodeA.transform.position);
                    }
                    else
                        l.nodeA.prevPosition = l.nodeA.transform.position;
                    if (!l.nodeB.locked)
                    {
                        l.nodeB.transform.position = center - l.length * direction / 2f;
                        l.armSprite.SetPosition(1, l.nodeB.transform.position);
                    }
                    else
                        l.nodeB.prevPosition = l.nodeB.transform.position;

                    // Debug.DrawLine(l.nodeA.transform.position, (Vector2)l.nodeA.transform.position + 10f * ((Vector2)l.nodeA.transform.position - l.nodeA.prevPosition).sqrMagnitude * ((Vector2)l.nodeA.transform.position - l.nodeA.prevPosition).normalized, Color.red);
                    // Debug.DrawLine(l.nodeB.transform.position, (Vector2)l.nodeB.transform.position + 10f * ((Vector2)l.nodeB.transform.position - l.nodeB.prevPosition).sqrMagnitude * ((Vector2)l.nodeB.transform.position - l.nodeB.prevPosition).normalized, Color.blue);
                }
            }
        }
    }
}
