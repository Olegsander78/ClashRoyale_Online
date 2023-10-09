using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class RotateImage : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.Rotate(0f, 0f, _speed * Time.deltaTime);
        }
    }
}
