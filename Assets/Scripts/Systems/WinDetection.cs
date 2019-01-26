using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;

public class WinDetection : ComponentSystem
{
    private ComponentGroup PositionsGroup;
    private ComponentGroup DistanceGroup;

    protected override void OnCreateManager() {
        PositionsGroup = GetComponentGroup(ComponentType.ReadOnly<Position>());
        DistanceGroup = GetComponentGroup(ComponentType.ReadOnly<DistanceGoal>());
    }

    protected override void OnUpdate(){
        var positions = PositionsGroup.GetComponentDataArray<Position>();

        if(positions.Length != 2){
            Debug.LogError($"Only 1 Player Position and 1 Goal Position allowed");
            return;
        }

        var minDistances = DistanceGroup.GetComponentDataArray<DistanceGoal>();

        if (minDistances.Length != 1) {
            Debug.LogError($"Only 1 Distance allowed");
            return;
        }

        var current = positions[0].Value;
        var goal = positions[1].Value;

        current.y = 0;
        goal.y = 0;

        var distance = math.distance(current, goal);
        
        if (distance < minDistances[0].Value){
            //Todo: Set Game State
            Debug.Log("You Win");
        }


        Debug.Log(distance);
    }

 
}
