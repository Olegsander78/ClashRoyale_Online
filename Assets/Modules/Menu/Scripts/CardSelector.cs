using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class CardSelector : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private AvailableDeckUI _availableDeckUI;
        [SerializeField] private SelectedDeckUI _selectedDeckUI;
        [Space(24), Header("Логика переключения канвасов")]
        [SerializeField] private GameObject _mainCanvas;
        [SerializeField] private GameObject _cardSelectCanvas;
        public IReadOnlyList<Card> AvailableCards { get => _availableCards; }
        public IReadOnlyList<Card> SelectedCards { get => _selectedCards; }

        private List<Card> _availableCards = new List<Card>();
        private List<Card> _selectedCards = new List<Card>();
        private int _selectToggleIndex = 0;

        private void OnEnable()
        {
            FillListFromManager();
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

        public void SaveChanges()
        {
            _deckManager.ChangesDeck(SelectedCards, CloseChangesWindows);
        }

        public void CancelChanges()
        {
            FillListFromManager();
            _selectedDeckUI.OnUpdatedCardList(SelectedCards);
            _availableDeckUI.UpdateCardsList(AvailableCards, SelectedCards);
            CloseChangesWindows();
        }

        public void CloseChangesWindows()
        {
            _cardSelectCanvas.SetActive(false);
            _mainCanvas.SetActive(true);
        }

        private void FillListFromManager()
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
    }
}
