using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera FPSCam;
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
