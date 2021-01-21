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

    //public RaycastHit[] hits;
    public Vector3 originCamPosition;
    public Vector3[] transforms;
    private int hitIndex = 0;

    private int frameCount = 0;

    void Start()
    {
        rb.GetComponent<Rigidbody>();
        Camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Transform>();
        FireStart = GameObject.FindGameObjectWithTag("FireStart").GetComponent<Transform>();

        /*Ray ray = new Ray(Camera.transform.position, Camera.transform.forward);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            rb.velocity = (hit.point - FireStart.position).normalized * speed;
        }
        else
        {
            rb.velocity = FireStart.forward * speed;
        }
        
        Debug.Log(rb.velocity); */

        
    }

    void Update()
    {
        if(hitIndex < transforms.Length)
        {         
            transform.position = Vector3.MoveTowards(transform.position, transforms[hitIndex], 2f); 
        }

        
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

    void OnTriggerEnter(Collider collider)
    {
        if(frameCount != Time.frameCount)
        {
            if (collider.gameObject.CompareTag("Ennemy"))
            {
                Debug.Log("Player triggers " + collider.transform.name);
                if (hitIndex == 0)
                {
                    collider.gameObject.GetComponent<Ennemy>().TakeDamage(10 * (hitIndex + 1), transforms[hitIndex], originCamPosition);
                }
                else
                {
                    collider.gameObject.GetComponent<Ennemy>().TakeDamage(10 * (hitIndex +1), transforms[hitIndex], transforms[hitIndex - 1]);
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.Log(collider.gameObject.transform.position);

                ++hitIndex;

                if (hitIndex < transforms.Length)
                {
                    Vector3 relativePos = (transforms[hitIndex] - transform.position).normalized;

                    Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    transform.rotation = rotation;
                    Debug.Log("a");
                }

                if (hitIndex >= transforms.Length)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        frameCount = Time.frameCount;
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "FireStart" || collision.gameObject.CompareTag("Ennemy"))
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
        }*/
    }
}
