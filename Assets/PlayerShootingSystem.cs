using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PlayerShootingSystem : SystemBase
{
    public event EventHandler OnShoot;
    protected override void OnCreate()
    {
        RequireForUpdate<Player>();
    }
    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, true);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Entity playerEntity = SystemAPI.GetSingletonEntity<Player>();
            EntityManager.SetComponentEnabled<Stunned>(playerEntity, false);
        }
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        //Unity.Collections.Allocator.Temp
        //Using WorldUpdateAllocator for max performance
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(WorldUpdateAllocator);
        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();
        foreach((RefRO<LocalTransform> localTransform, Entity entity) in SystemAPI.Query<RefRO<LocalTransform>>().WithAll<Player>().WithDisabled<Stunned>().WithEntityAccess())
        {
            
            Entity entitySpawn = entityCommandBuffer.Instantiate(spawnCubesConfig.cubeEntity);
            entityCommandBuffer.SetComponent(entitySpawn, LocalTransform.FromPosition(localTransform.ValueRO.Position));
            OnShoot?.Invoke(entity, EventArgs.Empty);
        }
        entityCommandBuffer.Playback(EntityManager);
    }
}
