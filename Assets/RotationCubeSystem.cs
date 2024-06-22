using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public partial struct RotationCubeSystem : ISystem
{
    void OnCreate(ref SystemState state)
    {
        //only update when it has entity has compoment RotationSpeed
        state.RequireForUpdate<RotationSpeed>();
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        /*foreach((RefRW<LocalTransform> refRWLocalTransform ,RefRO<RotationSpeed> refRORotationSpeed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
        {
            //RefRW = RW(set) + RO(get)
            //ValueRW == set
            //ValueRO == get
            refRWLocalTransform.ValueRW = refRWLocalTransform.ValueRO.RotateY(refRORotationSpeed.ValueRO.rotationSpeed * SystemAPI.Time.DeltaTime);
        }
        */
        RotationCubeJob rotationCubeJob = new RotationCubeJob { 
            deltaTime = SystemAPI.Time.DeltaTime,
        };
        //avoid this
        //rotationCubeJob.Schedule(state.Dependency).Complete();
        rotationCubeJob.Schedule();
        //for something is massive (like has over 200 entity)
        //rotationCubeJob.ScheduleParallel();
    }
    [BurstCompile]
    //using compoment like tag
    // run this funtion in all entity with no Player Compoment
    [WithNone(typeof(Player))]
    // run this funtion in all entity with Player Compoment
    //[WithAll(typeof(Player))]
    public partial struct RotationCubeJob : IJobEntity
    {
        public float deltaTime;
        //ref == RW
        //in == RO
        public void Execute(ref LocalTransform localTransform, in RotationSpeed rotationSpeed)
        {
            localTransform = localTransform.RotateY(rotationSpeed.rotationSpeed * deltaTime);
        }
    }
}
