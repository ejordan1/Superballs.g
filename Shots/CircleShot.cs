using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class CircleShot : MonoBehaviour, iMakeShoot
{
   
    //public int ballsInCircle = 200;
    public BallShooter ballShooter;


    public void Start()
    {
        ballShooter = GameObject.Find("BallShooter").GetComponent<BallShooter>();
    }

    public void fire()
    {
        int ballsInCircle = (int)PlayTimeSettings.ballSeparateness * 30;
        List<float3> circle = new List<float3>();
        float step = 2 * Mathf.PI / ballsInCircle;

        for (float theta = 0; theta < 2 * Mathf.PI; theta += step)
        {
            float x = 0 - 2 * Mathf.Cos(theta);
            float y = 0 - 2 * Mathf.Sin(theta);
            circle.Add(new float3(x, y, 0) * PlayTimeSettings.ballSeparateness / 1.3f);
        }
        ballShooter.fireBallGroup(circle);
    }
}
