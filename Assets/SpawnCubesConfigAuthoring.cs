using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnCubesConfigAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int amount;
    public class Baker : Baker<SpawnCubesConfigAuthoring>
    {
        public override void Bake(SpawnCubesConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnCubesConfig
            {
                cubeEntity = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                amount = authoring.amount,
            });
        }
    }
}
public struct SpawnCubesConfig : IComponentData
{
    public Entity cubeEntity;
    public int amount;
}
