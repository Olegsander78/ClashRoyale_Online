using UnityEngine;

namespace Registartion_Authorization
{
    public class UserInfo : MonoBehaviour
    {
        public static UserInfo Instance { get; private set; }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public int ID { get; private set; } = 4;

        public void SetID(int id)
        {
            ID = id;
        }
    }
}
