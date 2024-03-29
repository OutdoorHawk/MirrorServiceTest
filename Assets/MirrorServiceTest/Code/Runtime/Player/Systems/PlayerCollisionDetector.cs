﻿using System;
using UnityEngine;

namespace MirrorServiceTest.Code.Runtime.Player.Systems
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        public event Action<ControllerColliderHit> OnPlayerCollided;
        
        [SerializeField] private LayerMask _playerCollisionMask;

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (IsObjectInLayerMask(_playerCollisionMask, hit.gameObject.layer))
                OnPlayerCollided?.Invoke(hit);
        }

        private bool IsObjectInLayerMask(LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) > 0;
        }
    }
}