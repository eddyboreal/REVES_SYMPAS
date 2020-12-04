using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    public int max_reflection_count = 5;
    public float max_step_distance = 200f;
    
    public LineRenderer[] LaserLines;
    public float laser_width = 0.1f;
    public float laser_max_length = 5f;

    void Start()
    {
        initLaserLines(LaserLines);

        DrawLaser(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, max_reflection_count);
    }

    void Update()
    {
        if(Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0)
        {
            resetLaserLines(LaserLines);
        }
    }

    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);

        //DrawLaser(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, max_reflection_count);
        //DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, max_reflection_count);
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

    private void DrawLaser(Vector3 position, Vector3 direction, int remaining_reflections)
    {
        if (remaining_reflections == 0)
        {
            return;
        }

        Vector3 starting_pos = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, max_step_distance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * max_step_distance;
        }

        LaserLines[max_reflection_count - remaining_reflections].SetPosition(0, starting_pos);
        LaserLines[max_reflection_count - remaining_reflections].SetPosition(1, position);


        DrawLaser(position, direction, remaining_reflections - 1);
    }

    private void resetLaserLines(LineRenderer[] _laserLines)
    {
        foreach(LineRenderer laserLine in _laserLines)
        {
            laserLine.SetPosition(0, Vector3.zero * -46f);
            laserLine.SetPosition(1, Vector3.zero * -46f);
        }
    }

    private void initLaserLines(LineRenderer[] laserLines)
    {
        LaserLines = GetComponentsInChildren<LineRenderer>();

        foreach (LineRenderer laserLine in LaserLines)
        {
            laserLine.startWidth = laser_width;
            laserLine.endWidth = laser_width;
        }

        for (int i = max_reflection_count; i < LaserLines.Length; ++i)
        {
            LaserLines[i].gameObject.SetActive(false);
        }

        resetLaserLines(laserLines);
    }
}
