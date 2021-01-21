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

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Explode();
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

    private void Explode()
    {
        gameObject.SetActive(false);

        for(int i = 0; i< 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    createPieces(i, j, k);
                }
            }
        }

    }

    void createPieces(int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position + new Vector3(0.2f*x, 0.2f*y, 0.2f*z);
        piece.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = 0.2f;
    }

}
