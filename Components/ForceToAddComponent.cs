using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct ForceToAddComponent : IComponentData
{
    public float3 Value;
}
