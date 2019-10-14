//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;
//using Unity.Transforms;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Collections;
//using Unity.Burst;

////THERE IS AN ERROR GOING ON HERE AN I DON"T KNOW WHAT IT IS
//[UpdateBefore(typeof(SetBallGroupCenterSystem))]
//public class CalculateGroupCentersSystem : JobComponentSystem
//{
//    [BurstCompile]
//    struct CalculateCenter : IJobForEach<GroupCenterCalculationCaptionComponent>
//    {
//        public void Execute([ReadOnly] ref GroupCenterCalculationCaptionComponent calculationCaptionComponent)
//        {
//            Dictionary<int, float3> ballGroupCentersCalculated = new Dictionary<int, float3>();

//            foreach (KeyValuePair<int, float3> keyValuePair in BallShooter.ballGroupCumulatives)
//            {
//                ballGroupCentersCalculated.Add(keyValuePair.Key, BallShooter.ballGroupCumulatives[keyValuePair.Key] / BallShooter.ballGroupSizes[keyValuePair.Key]);
//            }
//            BallShooter.ballGroupCenters = ballGroupCentersCalculated;
//        }
//    }

//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var job = new CalculateCenter { };

//        return job.Schedule(this, inputDeps);
//    }
//}
