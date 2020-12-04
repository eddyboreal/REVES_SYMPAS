using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public int health = 50;
    public int damageDone = 10;
    public GameObject player;
    public bool waitDetectionToFollow = false;
    public float Distance;
    public float StartRaycastingDistance = 5f;

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
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        
    }

}
