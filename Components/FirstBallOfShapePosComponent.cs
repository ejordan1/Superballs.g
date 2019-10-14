using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public struct FirstBallOfShapePosComponent : IComponentData
{
    public float3 OriginalRelativePos;
    public float3 CurrentPos;
}
