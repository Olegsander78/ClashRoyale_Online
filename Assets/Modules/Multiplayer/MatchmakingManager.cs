using Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashRoyale
{
    public class MatchmakingManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mainManuCanvas;
        [SerializeField] private GameObject _matchmakingCanvas;
        [SerializeField] private GameObject _cancelButton;
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
    }
}
