using BH_Test_Project.Code.Infrastructure.Services;
using BH_Test_Project.Code.Runtime.MainMenu.Network;
using BH_Test_Project.Code.Runtime.MainMenu.Windows;
using UnityEngine;

namespace BH_Test_Project.Code.Infrastructure.StateMachine
{
    public class LoadMainMenuState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;
        private GameNetworkManager _gameNetworkManager;

        public LoadMainMenuState(IGameStateMachine gameStateMachine, IUIFactory uiFactory,
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _uiFactory.CreateUiRoot();
            InitializeMainMenu();
        }

        private void InitializeMainMenu()
        {
            MainMenuWindow window = _uiFactory.CreateMainMenuWindow();
            _gameNetworkManager = Object.Instantiate(_staticDataService.GetLobbyNetworkManager());
            window.Init(_gameNetworkManager);
            _gameNetworkManager.Init(_gameStateMachine);
        }

        public void Exit()
        {
    
        }
    }
}