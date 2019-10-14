using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ModelShot : MonoBehaviour
{
    BallShooter ballShooter;

    private Dictionary<string, List<float3>> modelVects;
    private Dictionary<string, modelInterpret> modelInterpretDict = new Dictionary<string, modelInterpret>();

    public void Awake()
    {
        modelInterpretDict.Add("lotus", new modelInterpret { sizeMult = 30, dividedBy = 2, y180Switch = true });
        modelInterpretDict.Add("humpback", new modelInterpret { sizeMult = 5, dividedBy = 1 });

        modelVects = HandleText.GetModelsFromFile("LaunchModels");
        //        Debug.Log("MODELVECTS COUNT: " + modelVects.Count);
        //resize:
        Dictionary<string, List<float3>> updatedWithSizeMultiplier = new Dictionary<string, List<float3>>();

        foreach(string key in modelVects.Keys)
        {
           
            if (modelInterpretDict.ContainsKey(key))
            {

                List<float3> newModelInterpretation = new List<float3>();
                updatedWithSizeMultiplier.Add(key, newModelInterpretation);
                for (int i = 0; i < modelVects[key].Count; i++)
                {

                    if (i % modelInterpretDict[key].dividedBy == 0)
                    {
                        float3 newPoint = modelVects[key][i] * modelInterpretDict[key].sizeMult;
                        if (modelInterpretDict[key].y180Switch)
                        {
                            newPoint.z = -newPoint.z;
                        }
                        newModelInterpretation.Add(newPoint);
                    }
                }
            }
            else
            {
                updatedWithSizeMultiplier.Add(key, modelVects[key]);
            }
        }
        modelVects = updatedWithSizeMultiplier;
    }

    public void Start()
    {
        ballShooter = GameObject.Find("BallShooter").GetComponent<BallShooter>();

    }


    public void fireHumpback()
    {
        ballShooter.fireBallGroup(modelVects["humpback"]);
    }

    public void fireLotus()
    {
        ballShooter.fireBallGroup(modelVects["lotus"]);
    }

    public struct modelInterpret
    {
        public float sizeMult;
        public int dividedBy;
        public bool y180Switch;
    }



}


//WAS IN MIDDLE OF GETTING LOTUS TO WORK