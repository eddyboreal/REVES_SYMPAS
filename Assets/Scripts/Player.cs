using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public int health = 30;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            gameObject.GetComponentInChildren<MouseLook>().CanMove = false;
            gameObject.GetComponent<PlayerMovement>().CanMove = false;
            gameObject.GetComponentInChildren<Blaster>().ResetBulletTime();
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("You died");
    }
}
