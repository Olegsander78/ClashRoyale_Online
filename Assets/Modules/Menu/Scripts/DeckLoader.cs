using Registartion_Authorization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class DeckLoader : MonoBehaviour
    {
        [SerializeField] private List<int> _availableCards = new List<int>();
        [SerializeField] private int[] _selectedCards = new int[2];

        public void Init()
        {
            var uri = URILibrary.MAIN + URILibrary.GETDECKINFO;

            Registartion_Authorization.Network.Instance.Post(uri,
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
            DeckData deckData = JsonUtility.FromJson<DeckData>(data);

            _selectedCards = new int[deckData.selectedIDs.Length];
            for (int i = 0; i < _selectedCards.Length; i++)
            {
                int.TryParse(deckData.selectedIDs[i], out _selectedCards[i]);
            }

            for (int i = 0; i < deckData.availableCards.Length; i++)
            {
                int.TryParse(deckData.availableCards[i].id, out int id);
                _availableCards.Add(id);
            }
        }
    }

    [Serializable]
    public class DeckData
    {
        public Availablecard[] availableCards;
        public string[] selectedIDs;
    }

    [Serializable]
    public class Availablecard
    {
        public string name;
        public string id;
    }

}
