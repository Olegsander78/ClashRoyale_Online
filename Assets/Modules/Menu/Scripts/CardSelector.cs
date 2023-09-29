using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class CardSelector : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private AvailableDeckUI _availableDeckUI;
        [SerializeField] private SelectedDeckUI _selectedDeckUI;
        public IReadOnlyList<Card> AvailableCards { get => _availableCards; }
        public IReadOnlyList<Card> SelectedCards { get => _selectedCards; }

        private List<Card> _availableCards = new List<Card>();
        private List<Card> _selectedCards = new List<Card>();
        private int _selectToggleIndex = 0;

        private void OnEnable()
        {
            _availableCards.Clear();
            for (int i = 0; i < _deckManager.AvailableCards.Count; i++)
            {
                _availableCards.Add(_deckManager.AvailableCards[i]);
            }

            _selectedCards.Clear();
            for (int i = 0; i < _deckManager.SelectedCards.Count; i++)
            {
                _selectedCards.Add(_deckManager.SelectedCards[i]);
            }
        }

        public void SetSelectToggleIndex(int index)
        {
            _selectToggleIndex = index;
        }

        public void SelectCard(int cardID)
        {
            _selectedCards[_selectToggleIndex] = _availableCards[cardID - 1];
            _selectedDeckUI.OnUpdatedCardList(SelectedCards);
            _availableDeckUI.UpdateCardsList(AvailableCards, SelectedCards);
        }
    }
}
