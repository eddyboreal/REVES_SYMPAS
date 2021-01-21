using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnnemy : Ennemy
{
    // Start is called before the first frame update
    public LineRenderer myLine;
    public float laserStartSize;
    public float laserGrowthMultiplier;


    public float laserDeGrowthValue;
    public float timeToDegrowthCompletely;
    public float timeToWaitBetweenEachFrameMultiplier;

    private RaycastHit lastHit;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {

        myLine.startWidth= laserStartSize;
        myLine.endWidth = laserStartSize;
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

    public void DrawLazer()
    {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            lastHit = hit;
            myLine.startWidth += Time.deltaTime * laserGrowthMultiplier;
            myLine.endWidth += Time.deltaTime * laserGrowthMultiplier;
            myLine.SetPosition(0, transform.position);
            myLine.SetPosition(1, hit.point);
        }
    }

    public void ResetLazer()
    {
        if (lastHit.collider.gameObject.CompareTag("Player"))
        {
            lastHit.collider.gameObject.GetComponent<Player>().TakeDamage(damageDone);
        }
        StartCoroutine(laserDegrowth());
    }

    IEnumerator laserDegrowth()
    {
        float counter = 0;
        while (counter <= timeToDegrowthCompletely)
        {
            counter += Time.deltaTime * timeToWaitBetweenEachFrameMultiplier;
            myLine.startWidth -= laserDeGrowthValue;
            myLine.endWidth -= laserDeGrowthValue;
            yield return new WaitForSeconds(Time.deltaTime * timeToWaitBetweenEachFrameMultiplier);
        }
        myLine.startWidth = laserStartSize;
        myLine.endWidth = laserStartSize;
        myLine.SetPosition(0, new Vector3(-500f, -500f, -500f));
        myLine.SetPosition(1, new Vector3(-500f, -500f, -500f));
    }
}
