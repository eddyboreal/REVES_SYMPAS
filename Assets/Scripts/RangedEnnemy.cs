﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnnemy : Ennemy
{
    // Start is called before the first frame update
    public LineRenderer myLine;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        myLine.startWidth= 0.1f;
        myLine.endWidth = 0.1f;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
    }

    public override void StartFollowing()
    {
        base.StartFollowing();
        StartCoroutine("FollowPlayer");
    }

    IEnumerator FollowPlayer()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        while (followingPlayer)
        {
            agent.destination = player.transform.position;
            yield return new WaitForSeconds(RefreshPlayerPosition);
        }
    }

    public void DrawLazer()
    {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            myLine.startWidth += Time.deltaTime * 0.1f;
            myLine.endWidth += Time.deltaTime * 0.1f;
            myLine.SetPosition(0, transform.position);
            myLine.SetPosition(1, hit.point);
        }
    }

    public void ResetLazer()
    {
        myLine.startWidth = 0.1f;
        myLine.endWidth = 0.1f;
        myLine.SetPosition(0, new Vector3(-500f, -500f, -500f));
        myLine.SetPosition(1, new Vector3(-500f, -500f, -500f));
    }
}
