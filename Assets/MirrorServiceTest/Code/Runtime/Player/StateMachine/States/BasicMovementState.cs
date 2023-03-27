using MirrorServiceTest.Code.Runtime.Animation;
using MirrorServiceTest.Code.Runtime.Player.Input;
using MirrorServiceTest.Code.Runtime.Player.Systems;
using UnityEngine;

namespace MirrorServiceTest.Code.Runtime.Player.StateMachine.States
{
    public class BasicMovementState : ITickableState
    {
        private readonly PlayerInput _playerInput;
        private readonly IPlayerStateMachine _stateMachine;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAnimator _playerAnimator;

        public BasicMovementState(IPlayerStateMachine stateMachine, PlayerMovement playerMovement,
            PlayerAnimator playerAnimator, PlayerInput playerInput)
        {
            _stateMachine = stateMachine;
            _playerMovement = playerMovement;
            _playerAnimator = playerAnimator;
            _playerInput = playerInput;
        }

        public void Enter()
        {
            _playerInput.OnDashPressed += ActivateDash;
        }

        public void Tick()
        {
            Vector2 currentInput = _playerInput.Movement.ReadValue<Vector2>();
            _playerMovement.UpdateInput(currentInput);
            _playerAnimator.SetPlayerSpeed(_playerMovement.GetNormalizedPlayerSpeed());
        }

        public void FixedTick()
        {
            _playerMovement.CalculateMovementVector();
            _playerMovement.ApplyMovement();
        }

        private void ActivateDash()
        {
            _stateMachine.Enter<DashState>();
        }

        public void Exit()
        {
            _playerInput.OnDashPressed -= ActivateDash;
        }
    }
}