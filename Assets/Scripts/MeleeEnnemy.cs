using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnnemy : Ennemy
{
    public float SuicideDistance;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int damage, Vector3 hitPosition, Vector3 rayOrigin)
    {
        base.TakeDamage(damage, hitPosition, rayOrigin);
    }

    public override void Die(Vector3 hitPosition, Vector3 rayOrigin)
    {
        base.Die(hitPosition, rayOrigin);
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
