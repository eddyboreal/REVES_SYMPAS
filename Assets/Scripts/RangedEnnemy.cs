using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnnemy : Ennemy
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
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
}
