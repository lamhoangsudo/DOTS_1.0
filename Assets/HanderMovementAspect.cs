using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct HanderMovementAspect : IAspect
{
    public readonly RefRW<LocalTransform> refRWLocalTransform;
    public readonly RefRO<RotationSpeed> refRORotationSpeed;
    public readonly RefRO<Movement> refROMovement;
}
