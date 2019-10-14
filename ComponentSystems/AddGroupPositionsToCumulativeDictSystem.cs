//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;
//using Unity.Transforms;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Collections;
//using Unity.Burst;


////The dictionary is reset in setballgroupcenters system because that is the next step after calculate group centers
//[UpdateBefore(typeof(CalculateGroupCentersSystem))]
//public class AddGroupPositionsToCumulativeDictSystem : JobComponentSystem
//{
//    [BurstCompile]
//    struct AddToCumes : IJobForEach<Translation, BallGroupIdComponent>
//    {
//        public void Execute(ref Translation translation, ref BallGroupIdComponent ballGroupId)
//        {
//            //can delete this once am sure this is working
//            if (!BallShooter.ballGroupCumulatives.ContainsKey(ballGroupId.Value))
//            {
//                throw new System.Exception("NO KEY FOR BALL GROUP ID: " + ballGroupId + " IN BALLSHOOTER GROUP CUMES DICT");
//            }
//            BallShooter.ballGroupCumulatives[ballGroupId.Value] += translation.Value;
//        }
//    }

//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var job = new AddToCumes { };

//        return job.Schedule(this, inputDeps);
//    }
//}


////