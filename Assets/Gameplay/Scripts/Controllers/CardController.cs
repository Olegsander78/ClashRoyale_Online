using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ClashRoyale
{
    public class CardController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private float _dragSize = 0.75f;

        private bool _isDragging = false;
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.localPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (false) return; // no mana
            _isDragging = true;

            transform.localScale = Vector3.one * _dragSize;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
                return;

            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDragging == false)
                return;
            _isDragging = false;

            transform.localPosition = _startPosition;
            transform.localScale = Vector3.one * _dragSize;
        }
    }
}
