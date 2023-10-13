using UnityEngine;

namespace ClashRoyale
{
    public class MenuSubscriber : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private SelectedDeckUI _selectedDeckUI;
        [SerializeField] private SelectedDeckUI _selectedDeckUITwo;
        [SerializeField] private SelectedDeckUI _selectedDeckUIMatchmaking;
        [SerializeField] private AvailableDeckUI _availableDeckUI;
        [SerializeField] private MatchmakingManager _matchmakingManager;

        private void Start()
        {
            _deckManager.OnUpdatedSelected += _selectedDeckUI.OnUpdatedCardList;
            _deckManager.OnUpdatedSelected += _selectedDeckUITwo.OnUpdatedCardList;
            _deckManager.OnUpdatedSelected += _selectedDeckUIMatchmaking.OnUpdatedCardList;
            _deckManager.OnUpdatedAvailable += _availableDeckUI.UpdateCardsList;

            _matchmakingManager.Subscribe();
        }

        private void OnDestroy()
        {
            _deckManager.OnUpdatedSelected -= _selectedDeckUI.OnUpdatedCardList;
            _deckManager.OnUpdatedSelected -= _selectedDeckUITwo.OnUpdatedCardList;
            _deckManager.OnUpdatedSelected -= _selectedDeckUIMatchmaking.OnUpdatedCardList;
            _deckManager.OnUpdatedAvailable -= _availableDeckUI.UpdateCardsList;

            _matchmakingManager.Unsubscribe();
        }
    }
}
