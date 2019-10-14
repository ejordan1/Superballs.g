using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//system right now is thorugh monobehaviors, doesn't need to be at all
public class BasicShot : MonoBehaviour, iMakeShoot
{
    public BallShooter ballShooter;

    public void Start()
    {
        ballShooter = GameObject.Find("BallShooter").GetComponent<BallShooter>();
    }

    public void fire()
    {
        ballShooter.fireSingleBall();
    }
}
