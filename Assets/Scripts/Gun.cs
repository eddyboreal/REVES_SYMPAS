using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;

    private LineRenderer laserLine;
    private Transform gunEnd;

    public Camera FPSCam;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    //use raycast to shoot
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
        {
            Ennemy ennemy = hit.transform.GetComponent<Ennemy>();
            if (ennemy)
            {
                ennemy.TakeDamage(damage);
            }
        }
    }
}
