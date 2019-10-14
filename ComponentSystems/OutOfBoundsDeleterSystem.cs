using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;


public class OutOfBoundsDeleterSystem : ComponentSystem
{

    protected override void OnUpdate()
    {

            Entities.ForEach((Entity entity, ref Translation translation) =>
            { 
                if ( ((Vector3)translation.Value).magnitude > 1000000 )
                {
                    EntityManager.DestroyEntity(entity);
                }
            });
            PlayTimeSettings.destroyAllOnNextFrame = false;

    }
}