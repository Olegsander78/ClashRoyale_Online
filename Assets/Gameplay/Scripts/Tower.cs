using UnityEngine;

namespace UnityRoyale
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float _radius = 2f;

        public float GetDistance(in Vector3 point)
        {
            return Vector3.Distance(transform.position, point) - _radius;
        }
    }
}
