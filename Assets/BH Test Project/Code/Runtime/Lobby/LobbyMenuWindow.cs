using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using static BH_Test_Project.Code.Infrastructure.Data.Constants;

namespace BH_Test_Project.Code.Runtime.Lobby
{
    public class LobbyMenuWindow : NetworkRoomPlayer
    {
        [SerializeField] private Button _readyButton;
        [SerializeField] private Button _leaveButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Transform _playerSlotsParent;
        [SerializeField] private int _minPlayersToStartGame = 2;

        private PlayerSlotView[] _playerSlots;
        private readonly SyncList<LobbyPlayer> _playersInLobby = new();

        public override void OnClientEnterRoom()
        {
            base.OnClientEnterRoom();
            if (isClient && isLocalPlayer)
                InitClient();
        }

        private void InitClient()
        {
            _playerSlots = _playerSlotsParent.GetComponentsInChildren<PlayerSlotView>(true);
            _leaveButton.onClick.AddListener(DisconnectLobby);

            if (isClient)
                _playersInLobby.Callback += OnPlayersListChanged;
            if (isServer)
                _startGameButton.gameObject.SetActive(true);

            CmdUpdatePlayersList();
        }

        [Command(requiresAuthority = false)]
        private void CmdUpdatePlayersList()
        {
            _playersInLobby.Add(new LobbyPlayer(netId, PlayerPrefs.GetString(PLAYER_NAME), false));
        }

        private void OnPlayersListChanged(SyncList<LobbyPlayer>.Operation op, int itemIndex, LobbyPlayer oldItem,
            LobbyPlayer newItem)
        {
            switch (op)
            {
                case SyncList<LobbyPlayer>.Operation.OP_ADD:
                    _playerSlots[itemIndex].ConnectPlayer(newItem.PlayerName);
                    break;
                case SyncList<LobbyPlayer>.Operation.OP_REMOVEAT:
                    _playerSlots[itemIndex].ClearPlayer();
                    break;
                case SyncList<LobbyPlayer>.Operation.OP_SET:
                    _playerSlots[itemIndex].SetReady(newItem.IsReady);
                    break;
            }
        }

        private bool IsEveryoneReady()
        {
            if (_playersInLobby.Count < _minPlayersToStartGame)
                return false;
            return _playersInLobby.All(player => player.IsReady);
        }

        private void DisconnectLobby()
        {
            if (isServer)
                NetworkServer.DisconnectAll();
            if (isClient)
                NetworkClient.Disconnect();
            gameObject.SetActive(false);

            Debug.Log("disconnect");
        }

        public override void OnClientExitRoom()
        {
            base.OnClientExitRoom();
            _leaveButton.onClick.RemoveListener(DisconnectLobby);
            _playersInLobby.Callback -= OnPlayersListChanged;
        }
    }
}