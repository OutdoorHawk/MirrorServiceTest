using System.Collections.Generic;
using MirrorServiceTest.Code.Infrastructure.Data;
using MirrorServiceTest.Code.Infrastructure.Services.Network;
using MirrorServiceTest.Code.Infrastructure.StaticData;
using MirrorServiceTest.Code.StaticData;
using UnityEngine;

namespace MirrorServiceTest.Code.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string GAME_STATIC_DATA_PATH = "GameStaticData";

        private readonly Dictionary<WindowID, WindowConfig> _windows = new();
        private GameNetworkService _gameNetworkService;
        private PlayerStaticData _playerStaticData;
        private WorldStaticData _worldStaticData;
        private GameStaticData _data;

        public void Load()
        {
            _data = Resources.Load<GameStaticData>(GAME_STATIC_DATA_PATH);
            LoadWindows();
            LoadNetworkManager();
            LoadPlayerStaticData();
            LoadWorldStaticData();
        }

        private void LoadWindows()
        {
            foreach (var window in _data.Windows)
                _windows.Add(window.ID, window);
        }

        private void LoadNetworkManager() =>
            _gameNetworkService = _data.ServicePrefab;

        private void LoadPlayerStaticData() =>
            _playerStaticData = _data.PlayerStaticData;

        private void LoadWorldStaticData() =>
            _worldStaticData = _data.WorldStaticData;

        public WindowConfig GetWindow(WindowID id) =>
            _windows.TryGetValue(id, out var windowConfig) ? windowConfig : null;

        public GameNetworkService GetLobbyNetworkManager() =>
            _gameNetworkService;

        public PlayerStaticData GetPlayerStaticData() =>
            _playerStaticData;

        public WorldStaticData GetWorldStaticData() =>
            _worldStaticData;
    }
}