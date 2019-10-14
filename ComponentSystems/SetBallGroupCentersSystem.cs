//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;
//using Unity.Transforms;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Collections;
//using Unity.Burst;

//[UpdateBefore(typeof(ReturnToGroupPosRelative))]
//[UpdateBefore(typeof(GroupImplodeSystem))]
//public class SetBallGroupCenterSystem : JobComponentSystem
//{
//    [BurstCompile]
//    struct SetCenter : IJobForEach<BallGroupCenterComponent, BallGroupIdComponent>
//    {
//        public float3 lastBallGroupCenter;
//        public void Execute(ref BallGroupCenterComponent ballGroupCenter, ref BallGroupIdComponent ballGroupId)
//        {

//            //this is fishy for getting static variable; it needs to do this somehow
//            ballGroupCenter.Value = lastBallGroupCenter;
            
//        }
//    }

//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        ResetBallGroupCumulativesDict();
//        var job = new SetCenter { lastBallGroupCenter = BallShooter.centerOfLastGroup };
//        return job.Schedule(this, inputDeps);
//    }

//    private void ResetBallGroupCumulativesDict()
//    {
//        foreach(int groupId in BallShooter.ballGroupCumulatives.Keys)
//        {
//            BallShooter.ballGroupCumulatives[groupId] = new float3(0, 0, 0);
//        }
//    }
//}