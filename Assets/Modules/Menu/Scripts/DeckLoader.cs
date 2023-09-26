using Registartion_Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class DeckLoader : MonoBehaviour
    {
        private List<int> _availableCards = new List<int>();
        private int[] _selectedCards = new int[5];

        public void Init()
        {
            Registartion_Authorization.Network.Instance.Post(URILibrary.GETDECKINFO,
                new Dictionary<string, string> { { "userID", UserInfo.Instance.ID.ToString() } },
                SuccesLoad, ErrorLoad
                );
        }

        private void ErrorLoad(string error)
        {
            Debug.LogError(error);
        }

        private void SuccesLoad(string data)
        {
            Debug.LogError(data);
        }
    }
}
