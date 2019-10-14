//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;
//using Unity.Transforms;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Collections;
//using Unity.Burst;

//public class TestEntityListIntoJobs : JobComponentSystem
//{
//    private EntityQuery m_Group;
//    protected override void OnCreate()
//    {
//        m_Group = GetEntityQuery(typeof(ForceComponent));
//    }


//    [BurstCompile]
//    struct MoveBall : IJobChunk
//    {
//        public void Execute(ref Translation translation, ref ForceToAddComponent forceToAdd, ref ForceComponent force)
//        {
//            force.Value += forceToAdd.Value;
//            translation.Value += force.Value;
//            forceToAdd.Value = new float3(0, 0, 0);
//        }
//    }

//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var job = new MoveBall { };

//        return job.Schedule(this, inputDeps);
//    }
//}


////one approach would be to create an entity query right after I make the objects and give them all a component that is just made, 
////and then make the query based on that component.

    ////could be an error when I remove that component and still try to use the query
    /// 
    /// 
    /// going try this instead: have only one float3 that is the current entity vector. each time a new group is created, that is replaced.
    /// this means all groups will always be with the last one that is created.