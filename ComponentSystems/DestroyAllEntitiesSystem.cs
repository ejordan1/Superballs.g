using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;


public class DestroyAllEntitiesSystem : ComponentSystem
{

    protected override void OnUpdate()
    {
        if (PlayTimeSettings.destroyAllOnNextFrame)
        {

                Entities.ForEach((Entity entity, ref ForceComponent force) =>
                {

                 //   if (UnityEngine.Random.Range(0, 4) > 1f)
                  //  {
                        EntityManager.DestroyEntity(entity);
                 //   }
                   
                });
            PlayTimeSettings.destroyAllOnNextFrame = false;
        }
    }
}