using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;

/// <summary>
/// this is all pretty trashy. probably some better optimization that could be done.
/// </summary>
[UpdateBefore(typeof(WhirlSystem))]
public class UpdatePosOfFirstBallInGroupSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (EntityManager.HasComponent<Translation>(BallShooter.firstBallOfLastEntityGroup)){
            BallShooter.firstBallOfLastEntityGroupCurrentPos = EntityManager.GetComponentData<Translation>(BallShooter.firstBallOfLastEntityGroup).Value;
        }
    }
}

//could convert this and the other thing to passing in the dict<int, float3> if no one responds in the forum.