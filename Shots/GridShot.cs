using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GridShot : MonoBehaviour, iMakeShoot
{
    public BallShooter ballShooter;
   
    public void Start()
    {
        ballShooter = GameObject.Find("BallShooter").GetComponent<BallShooter>();
    }

    public void fire()
    {
        List<float3> grid = new List<float3>();
        int xSize = PlayTimeSettings.gridSizeX;
        int ySize = PlayTimeSettings.gridSizeY;
        int zSize = PlayTimeSettings.gridSizeZ;


        float xAdjust = xSize / 2 * PlayTimeSettings.ballSeparateness;
        float yAdjust = ySize / 2 * PlayTimeSettings.ballSeparateness;
        float zAdjust = zSize / 2 * PlayTimeSettings.ballSeparateness;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                for (int k = 0; k < zSize; k++)
                {
                    grid.Add(new float3(i * PlayTimeSettings.ballSeparateness, j * PlayTimeSettings.ballSeparateness, k * PlayTimeSettings.ballSeparateness));
                }
            }
        }
        ballShooter.fireBallGroup(grid);
    }

    public void fireStartGame(int x, int y)
    {
        List<float3> grid = new List<float3>();
        int xSize = x;
        int ySize = y;
        int zSize = 1;


        float xAdjust = xSize / 2 * DefaultSettings.BallSeparateness;
        float yAdjust = ySize / 2 * DefaultSettings.BallSeparateness;
        float zAdjust = zSize / 2 * DefaultSettings.BallSeparateness;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                for (int k = 0; k < zSize; k++)
                {
                    grid.Add(new float3(i * 3, j * 3, k * 3));
                }
            }
        }
       
        ballShooter.fireBallGroup(grid);
    }
}
