using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class DeckManager : MonoBehaviour
    {
        public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> OnUpdatedAvailable;
        public event Action<IReadOnlyList<Card>> OnUpdatedSelected;

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
