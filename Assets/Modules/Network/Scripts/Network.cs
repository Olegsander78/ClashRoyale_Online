using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Registartion_Authorization
{
    public class Network : MonoBehaviour
    {
        public static Network Instance { get; private set; }

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

        public void Post(string uri, Dictionary<string, string> data, Action<string> success, Action<string> error = null)
            => StartCoroutine(PostRoutine(uri, data, success, error));

        private IEnumerator PostRoutine(string uri, Dictionary<string, string> data, Action<string> success, Action<string> error = null)
        {
            using(UnityWebRequest www = UnityWebRequest.Post(uri, data))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                    error?.Invoke(www.error);
                else
                    success?.Invoke(www.downloadHandler.text);
            }
        }
    }
}
