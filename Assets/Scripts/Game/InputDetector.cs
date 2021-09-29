using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Game
{
    public class InputDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Vector2 _inputVector;

        public static event Action<Vector2Int> InputDetected;

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputVector = new Vector2(eventData.position.x, eventData.position.y);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = eventData.position - _inputVector;
            _inputVector.Normalize();
            Vector2Int intVector = Vector2Int.RoundToInt(_inputVector);
            InputDetected?.Invoke(intVector);
        }
    }
}
