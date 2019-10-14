using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;


//This class deletes some entities until there are less than 50, then deletes the ones with higher priority
//The only issue is, if there are more shapes on the screen than 50, it will not ever delete those last ones 
public class DestroySomeEntitiesSystem : ComponentSystem
{

    protected override void OnUpdate()
    {
        if (PlayTimeSettings.destroySomeEntities)
        {
            int entityCount = Entities.ToEntityQuery().CalculateEntityCount();
            if (entityCount < 1)
            {
                return;
            }
            else if (entityCount < 5)
            {
                Entities.ForEach((Entity entity, ref ForceComponent force, ref DoNotDeletePriority doNotDeletePriority) =>
                {

                    EntityManager.DestroyEntity(entity);
                });
            }
            else
            {
                float numberOutOf1000ToDestroy = entityCount > 500 ? DefaultSettings.destorySomeEntitiesEachFrameFrom1000 : DefaultSettings.destorySomeEntitiesFinal100;

                Entities.ForEach((Entity entity, ref ForceComponent force, ref DoNotDeletePriority doNotDeletePriority) =>
                {
                    if (doNotDeletePriority.Value < 1)
                    {
                        if (UnityEngine.Random.Range(0, 1000) > numberOutOf1000ToDestroy)
                        {
                            EntityManager.DestroyEntity(entity);
                        }
                    }
                });

               
            }
        }
    }
}