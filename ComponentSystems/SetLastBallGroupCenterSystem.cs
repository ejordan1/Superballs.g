//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Unity.Entities;
//using Unity.Transforms;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Collections;
//using Unity.Burst;

///// <summary>
///// this is all pretty trashy. probably some better optimization that could be done.
///// </summary>
//[UpdateBefore(typeof(WhirlSystem))]
//public class SetFirstBallGroupCenterSystem : ComponentSystem
//{
//    protected override void OnUpdate()
//    {
//        float3 cumulative = new float3(0,0,0);
//        if (BallShooter.groupEntityLists.Count > 0)
//        {
//            for (int i = 0; i < BallShooter.groupEntityLists[BallShooter.groupEntityLists.Count - 1].Count; i++)
//            {
//                cumulative += EntityManager.GetComponentData<Translation>(BallShooter.groupEntityLists[BallShooter.groupEntityLists.Count - 1][i]).Value;
//            }
//            BallShooter.centerOfLastGroup = cumulative / BallShooter.groupEntityLists[BallShooter.groupEntityLists.Count - 1].Count;
//        }

//    }
//}

////could convert this and the other thing to passing in the dict<int, float3> if no one responds in the forum.