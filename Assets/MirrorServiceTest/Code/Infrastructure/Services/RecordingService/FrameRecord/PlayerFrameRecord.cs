﻿using MirrorServiceTest.Code.Runtime.Player.StateMachine;
using UnityEngine;

namespace MirrorServiceTest.Code.Infrastructure.Services.RecordingService
{
    public struct PlayerFrameRecord
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public Quaternion Rotation;
        public float DashRemainingDistance;
        public ITickableState StateMachineState;
        public AnimationLayers[] AnimationLayers;
    }

    public struct AnimationLayers
    {
        public AnimationClip CurrentAnimationClip;
        public float AnimationTimePlayed;
    }
}