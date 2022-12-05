using System;
using System.Collections.Generic;
using BH_Test_Project.Code.Infrastructure.DI;
using BH_Test_Project.Code.Runtime.Lobby;
using Mirror;
using UnityEngine;

namespace BH_Test_Project.Code.Infrastructure.Services.Network
{
    public interface IGameNetworkService : IService
    {
        event Action<NetworkConnectionToClient> OnServerReadyEvent;
        event Action OnRoomClientEnterEvent;
        event Action<string> OnRoomClientSceneChangedEvent;

        List<NetworkRoomPlayer> PlayersInRoom { get; }
        RoomPlayer RoomPlayerPrefab { get; }
        GameObject GamePlayerPrefab { get; }
        int MinPlayersToStart { get; }

        void CreateLobbyAsHost();
        void JoinLobbyAsClient(string address);
        void AddPlayerProfile( string playerName);
        void StopServer();
        void LoadGameLevel();
        void UpdateScoreTables();
    }
}