using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class MenuSubscriber : MonoBehaviour
    {
        [SerializeField] private DeckManager _deckManager;
        [SerializeField] private SelectedDeckUI _selectedDeckUI;

        private void Start()
        {
            _deckManager.OnUpdatedSelected += _selectedDeckUI.OnUpdatedCardList;
        }

        private void OnDestroy()
        {
            _deckManager.OnUpdatedSelected -= _selectedDeckUI.OnUpdatedCardList;
        }
    }
}
