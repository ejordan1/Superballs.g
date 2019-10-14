using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct DoNotDeletePriority : IComponentData
{
    public int Value;
}
