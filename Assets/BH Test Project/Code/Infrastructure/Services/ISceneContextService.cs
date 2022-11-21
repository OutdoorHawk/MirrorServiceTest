﻿using System.Collections.Generic;
using BH_Test_Project.Code.Infrastructure.DI;
using BH_Test_Project.Code.Infrastructure.Network;
using BH_Test_Project.Code.Runtime.Player.UI;
using UnityEngine;

namespace BH_Test_Project.Code.Infrastructure.Services
{
    public interface ISceneContextService: IService
    {
        void InitSceneContext();
        List<Transform> GetSceneSpawnPoints();
        NetworkPlayerSystem GetPlayerSystem();
        PlayerGameUI GetPlayerUI();
    }
}