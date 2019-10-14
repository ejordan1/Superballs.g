using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;

//Important: this whole system is in reference to the actual gravitional center of the ball group. All of those points are not exactly in relation to that,
//this will throw the shape towards where there is a bias. So: Need to have a system to determine the actual center from the points received. 
public class ReturnToLockedGroupPosSystem : JobComponentSystem
{
    [BurstCompile]
    struct AddPosition : IJobForEach<Translation, ForceToAddComponent, IndexOfGroupComponent, ForceComponent, PositionOfLockedGroupComponent>
    {
        public float returnToLockedGroupPos;
        public float lockedPositionMult;
        public void Execute([ReadOnly] ref Translation translation, ref ForceToAddComponent forceToAdd, [ReadOnly] ref IndexOfGroupComponent indexOfGroupComponent, [ReadOnly] ref ForceComponent forceComponent, [ReadOnly] ref PositionOfLockedGroupComponent positionOfLockedGroupComponent)
        {
            //does mod count of that group
            float3 targetPos = positionOfLockedGroupComponent.Value;
            //if (Mathf.Approximately(returnToLockedGroupPos, 0))
            //{
            //    return;
            //}
            //This could be worth consider in place to implode, or separate. not sure
            // forceToAdd.force += ((ballGroupCenter.ballGroupCenter + relativePosToLaunchGroup.Value) - translation.Value) * returnToRelPosStrength;

            //OLD VERSION
            //Vector3 directionToAdd = (relativePosToLaunchGroup.Value - (translation.Value - ballGroupCenter.ballGroupCenter));
            //Debug.Log(ballGroupCenter.ballGroupCenter.ToString());
            //SWITCHED AROUND CLEANER
            Vector3 directionToAdd = ((targetPos * lockedPositionMult) - translation.Value);
            //            Debug.Log(translation.Value  + "SDFSDFSDF");
            //THIS ONE IS JUST IN THE MIDDLE
            //Vector3 directionToAdd = (bStruct.relativeStartPos - midPoint);
            if (Vector3.Distance(translation.Value, targetPos) > .1f)
            {
                //Debug.Log(ballGroupCenter.ballGroupCenter.ToString()); center is correct
                float multiplier = 1;
                Vector3 shouldVelocity = multiplier * (directionToAdd).normalized * Mathf.Pow(directionToAdd.magnitude, .4f); //square it
                                                                                                                              //+ new Vector3(3.3f * multiplier, 3.3f * multiplier, 0)
                                                                                                                              //float accel = .4f;
                                                                                                                              //maybe should have its own variable not spaceMult
                                                                                                                              //closer it is, the more it should be similar to the should velocity:

                Vector3 force = returnToLockedGroupPos * ((shouldVelocity - (Vector3)(forceComponent.Value)).normalized *
                                              //this is the acceleration: the difference between should vect and current
                                              Mathf.Pow(Vector3.Distance(shouldVelocity, (Vector3)(forceComponent.Value)), 2) /
                                              Mathf.Pow(Mathf.Clamp(Vector3.Distance(targetPos, translation.Value), .3f, 5000),
                                                  1 / 3));


                //clamps at: between 0.5 and 2, 2, and 20
                //one setting for this is .2f, another is 2
                if (force.magnitude > .2f)
                {
                    force = force.normalized * .2f;
                }
                forceToAdd.Value += (float3)force;
            }
        }
    }

    [BurstCompile]
    struct NothingJob : IJobForEach<PlaceHolderComponent>
    {
        public void Execute([ReadOnly] ref PlaceHolderComponent placeHolderComponent)
        {

            //this is so that it doesn't take all those references for nothing
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //        Debug.Log("Return to ball pos relative system going");
        // if (!Mathf.Approximately(PlayTimeSettings.returnToGroupPosRelative, 0))
        {
            var job = new AddPosition { returnToLockedGroupPos = PlayTimeSettings.collapseStrength, lockedPositionMult = DefaultSettings.lockedGroupPosMult };
            return job.Schedule(this, inputDeps);
        }
        //else
        //{
        //    Debug.Log("NOTHIN FROM WHIRL");
        //    var job = new NothingJob { };
        //    return job.Schedule(this, inputDeps);
        //}
    }
}