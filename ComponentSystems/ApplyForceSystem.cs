using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;

public class ApplyForceSystem : JobComponentSystem
{
    [BurstCompile]
    struct MoveBall : IJobForEach<Translation, ForceToAddComponent, ForceComponent>
    {
        public float forceAppliedMult;
        public float forceMult;

        public void Execute(ref Translation translation, ref ForceToAddComponent forceToAdd, ref ForceComponent force)
        {

            force.Value += forceToAdd.Value * forceAppliedMult;
            //Debug.Log(forceToAdd.Value + ", originally, and with applied: " + forceToAdd.Value * PlayTimeSettings.forceAppliedMult);
            //Debug.Log(PlayTimeSettings.forceAppliedMult + " is fa m");
            //Debug.Log(PlayTimeSettings.forceMult + "is force m");
            translation.Value += force.Value * forceMult;

            forceToAdd.Value = new float3(0, 0, 0);

        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new MoveBall { forceAppliedMult = PlayTimeSettings.forceAppliedMult, forceMult = PlayTimeSettings.forceMult };
    

        return job.Schedule(this, inputDeps);
    }
}
