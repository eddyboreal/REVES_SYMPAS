using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour
{
    public int health = 50;
    public int damageDone = 10;
    public GameObject player;
    public bool waitDetectionToFollow = false;
    public bool followingPlayer = false;
    public float RefreshPlayerPosition;
    public float Distance;
    public float StartRaycastingDistance = 5f;

    public Material playerMat;
    public Material playerHitOrangeMat;
    public Material playerHitYellowMat;


    public float cubeScale;
    public float hitForce;
    public float explosionRadius;
    public float UpwardModifier;

    public GameObject cone;

    private GameObject myCone;
    private GameObject myCone2;
    private GameObject myCone3;
    private Vector3 myHitPosition;

    RaycastHit hit;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (waitDetectionToFollow)
        {
            Distance = Vector3.Distance(player.transform.position, transform.position);
        }
    }

    public virtual void TakeDamage(int damage, Vector3 hitPosition, Vector3 rayOrigin)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(hitPosition, rayOrigin);
        }
    }

    public virtual void Die(Vector3 hitPosition, Vector3 rayOrigin)
    {
        Explode(hitPosition, rayOrigin);
        //Destroy(gameObject);
    }

    public virtual void StartFollowing()
    {
        followingPlayer = true;
    }

        public bool PlayerOnSight()
    {
        Debug.DrawRay(transform.position, GetComponent<Ennemy>().player.transform.position - transform.position);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }   
        }
        return false;
    }

    private void Explode(Vector3 hitPosition, Vector3 rayOrigin)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        for (int i = 0; i< 7; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    
                    createPieces(i, j, k);
                }
            }
        }

        Debug.Log(hitPosition + " " + rayOrigin);
        myCone = Instantiate(cone, hitPosition, Quaternion.LookRotation((rayOrigin - hitPosition).normalized));
        myCone2 = Instantiate(cone, hitPosition, Quaternion.LookRotation((rayOrigin - hitPosition).normalized));
        myCone2.transform.localScale += new Vector3(0.1f,0.1f,0.1f);
        myCone3 = Instantiate(cone, hitPosition, Quaternion.LookRotation((rayOrigin - hitPosition).normalized));
        myCone3.transform.localScale += new Vector3(0.7f, 0.7f, 0.7f);
        myHitPosition = hitPosition;
        StartCoroutine(explosion());

    }

    void createPieces(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position - transform.localScale/2 + new Vector3(cubeScale * x, cubeScale * y, cubeScale * z);
        piece.transform.localScale = new Vector3(cubeScale, cubeScale, cubeScale);
   
        piece.GetComponent<MeshRenderer>().material = playerMat;
    }

    IEnumerator explosion()
    {
        Debug.Log("d");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("c");

        foreach (Collider hit in myCone3.GetComponent<ConeController>().colliderList)
        {

            hit.gameObject.GetComponent<MeshRenderer>().material = playerHitOrangeMat;
        }

        foreach (Collider hit in myCone2.GetComponent<ConeController>().colliderList)
        {

            hit.gameObject.GetComponent<MeshRenderer>().material = playerHitYellowMat;
        }

        foreach (Collider hit in myCone.GetComponent<ConeController>().colliderList)
        {

            hit.gameObject.AddComponent<Rigidbody>();
            hit.gameObject.GetComponent<Rigidbody>().mass = 0.2f;
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(hitForce, myHitPosition, explosionRadius, UpwardModifier);
                hit.isTrigger = true;
            }
        }
    }

}
