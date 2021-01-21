using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody rb;

    private Vector3 direction;
    private bool firstTime = false;

    public Transform Camera;
    public Transform FireStart;

    void Start()
    {
        rb.GetComponent<Rigidbody>();
        Camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Transform>();
        FireStart = GameObject.FindGameObjectWithTag("FireStart").GetComponent<Transform>();

        Ray ray = new Ray(Camera.transform.position, Camera.transform.forward);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            rb.velocity = (hit.point - FireStart.position).normalized * speed;
        }
        else
        {
            rb.velocity = FireStart.forward * speed;
        }
        
        Debug.Log(rb.velocity);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    void FixedUpdate()
    {
        if (firstTime)
        {
            //rb.AddForce(direction * speed);
            firstTime = false;
        }
    }

    void OnTriggerEnter(Collision collison)
    {
        if (collison.gameObject.CompareTag("Ennemy"))
        {
            collison.gameObject.GetComponent<Ennemy>().TakeDamage(10,Vector3.zero,Vector3.zero);
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "FireStart" || collision.gameObject.CompareTag("Ennemy"))
        {
            Debug.Log("Player collides " + collision.transform.name);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            //Ray ray = new Ray(collision.GetContact(0).point, Vector3.Reflect(transform.position, collision.GetContact(0).normal));

            SetDirection(Vector3.Reflect(transform.position, collision.GetContact(0).normal).normalized);
            rb.velocity = Vector3.zero;
            rb.velocity = direction * speed;

            Debug.Log(rb.velocity);
        }
    }
}
