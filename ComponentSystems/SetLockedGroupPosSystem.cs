//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;
//using Unity.Transforms;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Collections;
//using Unity.Burst;

//public class SetLockedGroupPosSystem : JobComponentSystem
//{
//    [BurstCompile]
//    struct MoveBall : IJobForEach<IndexOfGroupComponent, PositionOfLockedGroupComponent>
//    {
//        public void Execute(ref IndexOfGroupComponent indexOfGroup, ref PositionOfLockedGroupComponent positionOfLocked)
//        {

//            positionOfLocked.Value = StaticData.positionOfLockedGroups["BigSphere"][indexOfGroup.Value % StaticData.positionOfLockedGroups["BigSphere"].Count];
//        }
//    }

//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var job = new MoveBall { };

//        return job.Schedule(this, inputDeps);
//    }
//}
