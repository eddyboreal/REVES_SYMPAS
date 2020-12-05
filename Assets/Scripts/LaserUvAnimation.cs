using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUvAnimation : MonoBehaviour
{
    public LineRenderer Laser;
    public float i_speed;

    private Material m_material;
    private float m_time;

    void Start()
    {
        m_material = Laser.material;

        m_time = 0.0f;
    }

    void Update()
    {
        m_time += Time.deltaTime * i_speed;

        m_material.SetFloat("_Offset", Mathf.Repeat(m_time, 1.0f));
    }
}
