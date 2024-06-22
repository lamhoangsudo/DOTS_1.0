using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct MovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        MovementCubeJob movementCubeJob = new MovementCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        movementCubeJob.Schedule();
    }
    [BurstCompile]
    [WithNone(typeof(Player))]
    public partial struct MovementCubeJob : IJobEntity
    {
        public float deltaTime;
        public void Execute(ref LocalTransform localTransform, in Movement movement)
        {
            localTransform = localTransform.Translate(movement.movementVector * deltaTime);
        }
    }
}
