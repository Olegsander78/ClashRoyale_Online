using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Colyseus;

namespace Multiplayer
{
    public class MultiplayerManager : ColyseusManager<MultiplayerManager>
    {
        public event Action GetReadyReceived;
        public event Action<string> StartReceived;
        public event Action CancelStartReceived;

        private const string RoomName = "state_handler";
        private const string GetReadyKey = "GetReady";
        private const string StartKey = "Start";
        private const string CancelStartKey = "CancelStart";

        private ColyseusRoom<State> _room;
        private int _userId;

        public string ClientId => _room == null ? "" : _room.SessionId;


        public void Init(int userId)
        {
            _userId = userId;
            base.Awake();
            Instance.InitializeClient();
            DontDestroyOnLoad(gameObject);
        }


        public async Task Connect()
        {
            var data = new Dictionary<string, object>
            {
                { "id", _userId }
            };
            _room = await Instance.client.JoinOrCreate<State>(RoomName, data);

            _room.OnMessage<object>(GetReadyKey, _ => GetReadyReceived?.Invoke());
            _room.OnMessage<string>(StartKey, (jsonDecks) => StartReceived?.Invoke(jsonDecks));
            _room.OnMessage<object>(CancelStartKey, _ => CancelStartReceived?.Invoke());
        }


        public void Disconnect()
        {
            _room?.Leave();
            _room = null;
        }
    }
}