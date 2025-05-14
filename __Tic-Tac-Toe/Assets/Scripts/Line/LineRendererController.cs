using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace Line
{
    public class LineRendererController : MonoBehaviour
    {
        [Inject] private LineRenderer _lineRenderer;
        [SerializeField] private float extensionLength = 0.5f;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _lineRenderer.positionCount = 2;
        }


        public void SetCompleteLine(Vector3 start, Vector3 end)
        {
            var startPos = start;
            var endPos = end;
            var direction = (end - start);

            direction *= extensionLength;

            startPos -= direction;
            endPos += direction;

            var midPoint = (startPos + endPos) / 2;

            _lineRenderer.SetPosition(0, midPoint);
            _lineRenderer.SetPosition(1, midPoint);

            DOTween.To(() => midPoint, x => {
                midPoint = x;
                _lineRenderer.SetPosition(0, midPoint);
            }, startPos, .2f);

            DOTween.To(() => midPoint, x => {
                midPoint = x;
                _lineRenderer.SetPosition(1, midPoint);
            }, endPos, .2f);
        }

        public void EraseLine()
        {
            _lineRenderer.SetPosition(0, _lineRenderer.GetPosition(1));
        }

        public void ColorLine(Color color)
        {
            _lineRenderer.startColor = color;
            _lineRenderer.endColor = color;
        }
    }
}
