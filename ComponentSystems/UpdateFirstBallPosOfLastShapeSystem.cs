//using Unity.Mathematics;
//using Unity.Collections;
//using Unity.Burst;
//using Unity.Entities;
//using Unity.Transforms;
//using Unity.Jobs;


////[UpdateBefore(typeof(ReturnToGroupPosRelative))] //dont think it needs either of these
////[UpdateBefore(typeof(GroupImplodeSystem))]
////[UpdateBefore(typeof(WhirlToRecentGroupEntity))]
//public class UpdateFirstBallPosOfLastShapeSystem : JobComponentSystem
//{
//    [BurstCompile]
//    struct UpdateFirstOfRecent : IJobForEach<FirstBallOfRecentShapePosComponent>
//    {
//        public float3 currentPos;
//        public float3 originalRelativePos;
//        public void Execute(ref FirstBallOfRecentShapePosComponent firstBallOfShapePosComponent)
//        {
//            firstBallOfShapePosComponent.CurrentPos = currentPos;
//            firstBallOfShapePosComponent.OriginalRelativePos = originalRelativePos;

//        }
//    }

//        [BurstCompile]
//        struct NothingJob : IJobForEach<PlaceHolderComponent>
//        {
//            public void Execute([ReadOnly] ref PlaceHolderComponent placeHolderComponent)
//            {
//                //this is so that it doesn't take all those references for nothing
//            }
//        }

//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {

    
//            var job = new UpdateFirstOfRecent
//            {
//                currentPos = EntityManager.GetComponentData<Translation>(BallShooter.lastBallGroupFirstEntity).Value,
//                originalRelativePos = EntityManager.GetComponentData<RelativePosToLaunchGroup>(BallShooter.lastBallGroupFirstEntity).Value
//            };
//            return job.Schedule(this, inputDeps);

//    }
//}
