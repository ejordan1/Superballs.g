using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;

public class GroupImplodeSystem : JobComponentSystem
{
    [BurstCompile]
    struct AddPosition : IJobForEach<BallGroupCenterComponent, Translation, ForceToAddComponent>
    {

        public float implodeStrength;
        public void Execute([ReadOnly] ref BallGroupCenterComponent ballGroupCenter, [ReadOnly] ref Translation translation, ref ForceToAddComponent forceToAdd)
        {
               forceToAdd.Value += (ballGroupCenter.Value - translation.Value) * implodeStrength;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
            
            var job = new AddPosition { implodeStrength = PlayTimeSettings.getGroupImplodeStrength() };
            return job.Schedule(this, inputDeps);
    }
}

//figure out how to optimize to not have to send in all the array sizes each time