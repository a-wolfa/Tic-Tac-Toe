using System;
using UnityEngine;

namespace Line
{
    public class LineRendererController : MonoBehaviour
    {
        public Transform pointA;
        public Transform pointB;
    
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
        }

        private void Update()
        {
            _lineRenderer.SetPosition(0, pointA.position);
            _lineRenderer.SetPosition(1, pointB.position);
        }
    }
}
