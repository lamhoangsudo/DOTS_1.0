using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnCubeSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubesConfig>();
    }

    protected override void OnUpdate()
    {
        this.Enabled = false;
        SpawnCubesConfig spawnCubesConfig = SystemAPI.GetSingleton<SpawnCubesConfig>();
        for (int i = 0; i < spawnCubesConfig.amount; i++)
        {
            //Baker run only one time not run every single time when instantiate prefab
            Entity entitySpawn = EntityManager.Instantiate(spawnCubesConfig.cubeEntity);
            //should use this for performance
            /*
            SystemAPI.SetComponent(entitySpawn, new LocalTransform
            {

            });
            */
            //Avoid using setcompoment and instead try to think of ways to make active with inactive components
            EntityManager.SetComponentData(entitySpawn, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-10f, 5f), 0.6f, UnityEngine.Random.Range(-4f, 7f)),
                Rotation = Quaternion.identity,
                Scale = 1f
            });
            //Same
            //LocalTransform newLocalTransform = LocalTransform.FromPosition(new float3(UnityEngine.Random.Range(-10f, 5f), 0.6f, UnityEngine.Random.Range(-4f, 7f)));
        }
    }
}
