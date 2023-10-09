using Registartion_Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Network = Registartion_Authorization.Network;

namespace ClashRoyale
{
    public class RatingManager : MonoBehaviour
    {
        [SerializeField] private Text _ratingText; 
        private void Start()
        {
            InitRating();
        }

        private void InitRating()
        {
            var uri = URILibrary.MAIN + URILibrary.GETRATING;
            var data = new Dictionary<string, string>()
            {
                {"userID", UserInfo.Instance.ID.ToString() }
            };

            Network.Instance.Post(uri, data, Success, Error);
        }

        private void Success(string obj)
        {
            String[] result = obj.Split('|');
            if(result.Length != 3)
            {
                Error("Длинна массива != 3 " + obj);
                return;
            }

            if (result[0] != "ok")
            {
                Error("Strange things"+ obj);
                return;
            }

            _ratingText.text = $"<color=green>{result[1]}</color> : <color=red>{result[2]}</color>";
        }

        private void Error(string obj)
        {
            Debug.LogError(obj);
        }
    }
}
