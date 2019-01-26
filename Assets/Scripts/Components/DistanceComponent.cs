using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct DistanceGoal : IComponentData {
    public float         Value;
}
    
    public class DistanceComponent : ComponentDataWrapper<DistanceGoal>
{
    
}


