using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public int health = 50;
    public int damageDone = 10;
    public GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

}
