using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;


//it seems like using this many dependenices makes for issues, even when it isn't doing anything. CURRENTLY OFF BECAUSE NOT STTING FIRST BALL OF SHAPE. WAIT UNTIL FORUM RESPONSE.

//Important: this whole system is in reference to the actual gravitional center of the ball group. All of those points are not exactly in relation to that,
//this will throw the shape towards where there is a bias. So: Need to have a system to determine the actual center from the points received. 
public class WhirlToCenterSystem : JobComponentSystem
{
    [BurstCompile]
    struct AddPosition : IJobForEach<BallGroupCenterComponent, Translation, ForceToAddComponent, RelativePosToLaunchGroup, ForceComponent, BallGroupIdComponent>
    {
        public float3 firstBallInGroupOriginalRelativePos;
        public float3 firstBallInGroupCurrentPos;
        //public float3 ballGroupCenterValue;
        public float whirlStrength;

        public void Execute([ReadOnly] ref BallGroupCenterComponent ballGroupCenter, [ReadOnly] ref Translation translation,
        ref ForceToAddComponent forceToAdd, [ReadOnly] ref RelativePosToLaunchGroup relativePosToLaunchGroup,
            [ReadOnly] ref ForceComponent forceComponent, [ReadOnly] ref BallGroupIdComponent ballGroupIdComponent)
        {
            //if (Mathf.Approximately(whirlStrength, 0))
            //{
            //    return;
            //}
            //GOING TO GET FROM BALL SHOOTER DICT CURRENT POSITION OF THAT BALL ID
            //have access to original positions, so can find original vext through that
            float3 currentVect = new float3(0, 0, .0001f) + translation.Value;
            float3 originalVect = new float3(0, 0, .0001f);

            // BallClass bc = parentObjects[parent][0];
            originalVect += firstBallInGroupOriginalRelativePos;
            //originalVect += new float3(0, 0, 0);
            //THE ORIGINAL VECT IS NOT IN RESPECT TO MIDPOINT
            // currentVect += (firstBallInGroupCurrentPos - ballGroupCenter.Value); 
            // currentVect += (new float3(0,0,0)); 

            Quaternion q1 = Quaternion.LookRotation(originalVect);
            //            Debug.Log(originalVect.ToString());

            Quaternion q2 = Quaternion.LookRotation(currentVect);
            //          Debug.Log(currentVect.ToString());

            Quaternion q3 = Quaternion.Inverse(q1) * q2;

            //this is not that complicated dont get caught up

            //directionToAddwithBallSeparateness
            Vector3 relativeStartWithBallSep = relativePosToLaunchGroup.Value;
            //where does position of current rotate go in here
            Vector3 directionToAdd = (q3 * (Vector3)relativeStartWithBallSep - (Vector3)(translation.Value - ballGroupCenter.Value));

            if (Vector3.Distance(translation.Value, ballGroupCenter.Value) > .01f) //maekes no sense atm
            {

                //1.5 is the space accell        5                               
                Vector3 shouldVelocity = 5 * (directionToAdd).normalized * Mathf.Pow(directionToAdd.magnitude, 0.1f); //square it



                //ALL THREE OF THESE ARE WORTH CONSIDERING
                //closer it is, the more it should be similar to the should velocity:
                //Vector3 forceAdded = whirlStrength * ((shouldVelocity).normalized *
                ////this is the acceleration: the difference between should vect and current
                //shouldVelocity.magnitude /
                //Mathf.Pow(Mathf.Clamp(Vector3.Distance(ballGroupCenter.Value, translation.Value), .3f, 500),
                //1 / 3));

                Vector3 forceAdded = whirlStrength * ((shouldVelocity).normalized *
                //this is the acceleration: the difference between should vect and current
                Mathf.Pow(shouldVelocity.magnitude, 2) /
                Mathf.Pow(Mathf.Clamp(Vector3.Distance(ballGroupCenter.Value, translation.Value), .3f, 500),
                1 / 3));

                //Vector3 forceAdded = whirlStrength * ((shouldVelocity - (Vector3)forceComponent.Value).normalized *
                ////this is the acceleration: the difference between should vect and current
                //Mathf.Pow(Vector3.Distance(shouldVelocity, (Vector3)forceComponent.Value), 2) /
                //Mathf.Pow(Mathf.Clamp(Vector3.Distance(ballGroupCenter.Value, translation.Value), .3f, 500),
                //1 / 3));


                //totally haky solution: clamp the force added:
                if (forceAdded.magnitude > 10)
                {
                    forceAdded = forceAdded.normalized * 10;
                }
                forceToAdd.Value += (float3)forceAdded;
            }


        }
    }

    [BurstCompile]
    struct NothingJob : IJobForEach<PlaceHolderComponent>
    {
        public void Execute(ref PlaceHolderComponent placeHolderComponent)
        {
            //this is so that it doesn't take all those references for nothing
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        // if (!Mathf.Approximately(PlayTimeSettings.whirlStrength, 0))
        // {
        var job = new AddPosition
        {
            firstBallInGroupCurrentPos = new float3(0, 0, 0), // BallShooter.firstBallOfLastEntityGroupCurrentPos,
            firstBallInGroupOriginalRelativePos = BallShooter.firstBallOfLastEntityGroupOriginalRelativePos,
            whirlStrength = PlayTimeSettings.waffleStrength
        };
        return job.Schedule(this, inputDeps);
        //}
        //else
        //{
        //    var job = new NothingJob { };
        //    return job.Schedule(this, inputDeps);
        //}
    }
}