using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace ClashRoyale
{
    public class CardsInGame : MonoBehaviour
    {
        public static CardsInGame Instance { get; private set; }

        public ReadOnlyDictionary<string, Card> _playerDeck { get; private set; }
        public ReadOnlyDictionary<string, Card> _enemyDeck { get; private set; }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetDeck(string[] playerCards, string[] enemyCards)
        {
            DeckManager deckManager = FindObjectOfType<DeckManager>();
            bool player = deckManager.TryGetDeck(playerCards, out Dictionary<string, Card> playerDeck);
            bool enemy = deckManager.TryGetDeck(enemyCards, out Dictionary<string, Card> enemyDeck);

            if(player == false || enemy == false)
            {
                Debug.LogError("Не удалось загрузить колоду!");
            }

            _playerDeck = new ReadOnlyDictionary<string, Card>(playerDeck);
            _enemyDeck = new ReadOnlyDictionary<string, Card>(enemyDeck);
        }
    }
}
