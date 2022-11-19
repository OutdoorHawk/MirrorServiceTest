using System;
using BH_Test_Project.Code.Runtime.Animation;
using BH_Test_Project.Code.Runtime.CameraLogic;
using BH_Test_Project.Code.Runtime.Player.Input;
using BH_Test_Project.Code.Runtime.Player.Movement;
using BH_Test_Project.Code.Runtime.Player.StateMachine;
using BH_Test_Project.Code.Runtime.Player.StateMachine.States;
using Mirror;
using UnityEngine;

namespace BH_Test_Project.Code.Runtime.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerCollisionDetector))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private CameraFollow _cameraFollowPrefab;

        private CameraFollow _cameraFollow;
        private PlayerInput _playerInput;
        private PlayerMovement _playerMovement;
        private PlayerAnimator _animator;
        private PlayerCollisionDetector _collisionDetector;
        private PlayerGameStatus _playerGameStatus;
        private IPlayerStateMachine _playerStateMachine;
        
        private void Start()
        {
            Init();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Init()
        {
            if (isClient && isLocalPlayer)
                CreateSystems();
            InitSystems();
            _playerInput.EnableAllInput();
        }

        private void CreateSystems()
        {
            Animator animator = GetComponent<Animator>();
            CharacterController characterController = GetComponent<CharacterController>();
            _collisionDetector = GetComponent<PlayerCollisionDetector>();
            _playerInput = new PlayerInput();
            _animator = new PlayerAnimator(animator);
            _cameraFollow = Instantiate(_cameraFollowPrefab);
            _playerMovement = new PlayerMovement(_playerData, characterController, transform, _cameraFollow, this);
            _playerStateMachine = new PlayerStateMachine(_playerMovement, _playerInput, _animator, _collisionDetector);
            _playerGameStatus = new PlayerGameStatus(_playerData, this);
        }

        private void InitSystems()
        {
            _playerInput.Init();
            _cameraFollow.Init(_playerInput, _playerData, transform);
            _playerStateMachine.Enter<BasicMovementState>();
            _collisionDetector.Init(_playerData.PlayerCollisionMask);
        }

        private void Update()
        {
            _playerStateMachine.Tick();
        }

        public void HitPlayer()
        {
            _playerGameStatus.PlayerHit();
        }

        private void OnDestroy()
        {
            DisposeSystems();
        }

        private void DisposeSystems()
        {
            _playerStateMachine.CleanUp();
            Destroy(_cameraFollow);
        }
    }
}