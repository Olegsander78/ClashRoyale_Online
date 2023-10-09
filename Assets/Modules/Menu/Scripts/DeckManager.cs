using Registartion_Authorization;
using System;
using System.Collections.Generic;
using UnityEngine;
using Network = Registartion_Authorization.Network;

namespace ClashRoyale
{
    public class DeckManager : MonoBehaviour
    {
        public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> OnUpdatedAvailable;
        public event Action<IReadOnlyList<Card>> OnUpdatedSelected;

        [SerializeField] private GameObject _lockScreenCanvas;
        [SerializeField] private Card[] _cards;
        [SerializeField] private List<Card> _availableCards = new List<Card>();
        [SerializeField] private List<Card> _selectedCards = new List<Card>();
        [SerializeField] private AvailableDeckUI _availableDeckUI;

        public IReadOnlyList<Card> AvailableCards { get => _availableCards;}
        public IReadOnlyList<Card> SelectedCards { get => _selectedCards;}

        public void Init(List<int> availableCardIndexes, int[] selectedCardIndexes)
        {
            for (int i = 0; i < availableCardIndexes.Count; i++)
            {
                _availableCards.Add(_cards[availableCardIndexes[i]]);
            }

            for (int i = 0; i < selectedCardIndexes.Length; i++)
            {
                _selectedCards.Add(_cards[selectedCardIndexes[i]]);
            }

            OnUpdatedAvailable?.Invoke(AvailableCards, SelectedCards);
            OnUpdatedSelected?.Invoke(SelectedCards);

            _lockScreenCanvas.SetActive(false);
        }

        public void ChangesDeck(IReadOnlyList<Card> selectedCards, Action success)
        {
            _lockScreenCanvas.SetActive(true);
            int[] IDs = new int[selectedCards.Count];
            for (int i = 0; i < selectedCards.Count; i++)
            {
                IDs[i] = selectedCards[i].id;
            }

            string json = JsonUtility.ToJson(new Wrapper(IDs));

            var uri = URILibrary.MAIN + URILibrary.SETSELECTDECK;
            var data = new Dictionary<string, string> 
            { 
                { "userID", UserInfo.Instance.ID.ToString() },
                { "json", json }
            };

            success += () =>
            {
                for (int i = 0; i < _selectedCards.Count; i++)
                {
                    _selectedCards[i] = selectedCards[i];
                }
                OnUpdatedSelected?.Invoke(SelectedCards);
            };

            Network.Instance.Post(uri, data, (s)=> SendSuccess(s, success), Error);
        }

        private void Error(string obj)
        {
            Debug.LogError("Неудачная попытка отправки новой колоды" + obj);
            _lockScreenCanvas.SetActive(false);
        }

        private void SendSuccess(string obj, Action success)
        {
            if(obj != "ok")
            {
                Error(obj);
                return;
            }

            success?.Invoke();
            _lockScreenCanvas.SetActive(false);
        }

        [Serializable]
        private class Wrapper
        {
            public int[] IDs;

            public Wrapper(int[] IDs)
            {
                this.IDs = IDs;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _availableDeckUI.SetAllCardsCount(_cards);
        }
#endif
    }

    [Serializable]
    public class Card
    {
        [field:SerializeField] public string name { get; private set; }
        [field:SerializeField] public int id { get; private set; }
        [field:SerializeField] public Sprite sprite { get; private set; }
    }
}
