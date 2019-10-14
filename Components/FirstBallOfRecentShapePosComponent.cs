using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
public struct FirstBallOfRecentShapePosComponent : IComponentData
{
    public float3 OriginalRelativePos;
    public float3 CurrentPos;
}
