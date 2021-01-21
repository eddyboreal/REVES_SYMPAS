using UnityEngine;

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
    public float cubeScale;
    public float hitForce;
    public float explosionRadius;
    public float UpwardModifier;

    public GameObject cone;

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
        gameObject.SetActive(false);

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

        //Instantiate(cone, hitPosition, Quaternion.FromToRotation(-Vector3.forward, transform.position -  hitPosition));
        Vector3 a = hitPosition + Vector3.Normalize(hitPosition - rayOrigin) * 5;
        Instantiate(cone, hitPosition, Quaternion.FromToRotation(-Vector3.forward, a - hitPosition));

        Collider[] colliders = Physics.OverlapSphere(hitPosition, 10);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(hitForce, hitPosition, explosionRadius, UpwardModifier);
            }
        }

    }

    void createPieces(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position - transform.localScale/2 + new Vector3(cubeScale * x, cubeScale * y, cubeScale * z);
        piece.transform.localScale = new Vector3(cubeScale, cubeScale, cubeScale);

        if (!piece.GetComponent<Rigidbody>())
        {
            piece.AddComponent<Rigidbody>();
            piece.GetComponent<Rigidbody>().mass = 0.2f;
        }
            piece.GetComponent<MeshRenderer>().material = playerMat;
    }

}
