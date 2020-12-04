using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    public int max_reflection_count = 5;
    public float max_step_distance = 200f;
    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);

        DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, max_reflection_count);
    }

    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int remaining_reflections)
    {
        if(remaining_reflections == 0)
        {
            return;
        }

        Vector3 starting_pos = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, max_step_distance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * max_step_distance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(starting_pos, position);


        DrawPredictedReflectionPattern(position, direction, remaining_reflections - 1);
    }
}
