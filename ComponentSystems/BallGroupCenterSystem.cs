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
//[UpdateBefore(typeof(SetBallGroupCenterSystem))]
//public class BallGroupCenterSystem : ComponentSystem
//{
//    protected override void OnUpdate()
//    {
//        BallShooter.ballGroupCenters = CalculateCenters();
//    }

//    private Dictionary<int, float3> CalculateCenters()
//    {
//        Dictionary<int, float3> ballGroupCumesPosition = new Dictionary<int, float3>();
//        foreach(KeyValuePair<int, List<Entity>> keyValuePair in BallShooter.groupEntityLists)
//        {
//                int groupId = keyValuePair.Key;
//                //int totalActualEntitiesCount = 0;
//                float3 cumulativePos = new float3(0, 0, 0);
//                for(int i = 0; i < keyValuePair.Value.Count; i++)
//                {
//                    //here could be a check to see if the entity still exists
//                    cumulativePos += EntityManager.GetComponentData<Translation>(BallShooter.groupEntityLists[keyValuePair.Key][i]).Value;
//                }
//            float3 centerPos = cumulativePos / BallShooter.groupEntityLists[groupId].Count;
//            ballGroupCumesPosition.Add(groupId, centerPos);
//        }
//        return ballGroupCumesPosition;
//    }

//    private void printDict(Dictionary<int, float3> dict)
//    {

//        foreach (KeyValuePair<int, float3> keyValuePair in dict)
//        {
//            Debug.Log("Key: " + keyValuePair.Key + ", VALUE: " + dict[keyValuePair.Key]);
//        }
//    }
    
//}

////could convert this and the other thing to passing in the dict<int, float3> if no one responds in the forum.