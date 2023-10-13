using Multiplayer;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClashRoyale
{
    public class MatchmakingManager : MonoBehaviour
    {
        [Serializable]
        public class Decks
        {
            public string player1ID;
            public string[] player1;
            public string[] player2;
        }

        [SerializeField] private string _gameSceneName = "Main";
        [SerializeField] private GameObject _mainManuCanvas;
        [SerializeField] private GameObject _matchmakingCanvas;
        [SerializeField] private GameObject _cancelButton;

        public void Subscribe()
        {
            MultiplayerManager.Instance.GetReadyReceived += GetReady;
            MultiplayerManager.Instance.StartReceived += StartGame;
            MultiplayerManager.Instance.CancelStartReceived += CancelStart;
        }

        public void Unsubscribe()
        {
            MultiplayerManager.Instance.GetReadyReceived -= GetReady;
            MultiplayerManager.Instance.StartReceived -= StartGame;
            MultiplayerManager.Instance.CancelStartReceived -= CancelStart;
        }

        public async void FindOpponent()
        {
            _cancelButton.SetActive(false);
            _mainManuCanvas.SetActive(false);
            _matchmakingCanvas.SetActive(true);

            await MultiplayerManager.Instance.Connect();
            _cancelButton.SetActive(true);
        }

        public void CancelFind()
        {
            _mainManuCanvas.SetActive(true);
            _matchmakingCanvas.SetActive(false);

            MultiplayerManager.Instance.Disconnect();
        }        

        private void GetReady()
        {
            _cancelButton.SetActive(false);
        }

        private void StartGame(string jsonDecks)
        {
            Decks decks = JsonUtility.FromJson<Decks>(jsonDecks);
            string[] playDeck;
            string[] enemyDeck;
            if(decks.player1ID == MultiplayerManager.Instance.ClientId)
            {
                playDeck = decks.player1;
                enemyDeck = decks.player2;
            }
            else
            {
                playDeck = decks.player2;
                enemyDeck = decks.player1;
            }
            CardsInGame.Instance.SetDeck(playDeck, enemyDeck);

            SceneManager.LoadScene(_gameSceneName);
        }
        
        private void CancelStart()
        {
            _cancelButton.SetActive(true);
        }
    }
}
