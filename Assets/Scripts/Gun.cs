using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;

    [SerializeField, Range(0, 1f)]
    private float fire_cooldown = 1f;
    public float fire_cooldown_counter = 1f;

    private bool fire_pressed = false;

    public GameObject LaserEmitter;
    private ProjectileEmitter projectileEmitter;

    public Camera FPSCam;

    void Start()
    {
        projectileEmitter = LaserEmitter.GetComponent<ProjectileEmitter>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (fire_pressed)
            {
                fire_pressed = false;
                //projectileEmitter.resetLaserLines();
            }
            else
            {
                fire_pressed = true;
            }
        }

        if (fire_pressed)
        {
            fire_cooldown_counter -= Time.deltaTime;
            projectileEmitter.DrawLaser(FPSCam.transform.position + FPSCam.transform.forward * 0.75f, FPSCam.transform.forward, projectileEmitter.max_reflection_count);
            if(fire_cooldown_counter <= 0)
            {
                Debug.Log("Shoot");
                Shoot();
                fire_cooldown_counter = fire_cooldown;
            }
        }
    }

    //OnFire
        //lancer un compteur
            //dessiner la trajectoire
        //a la fin du compteur
            //tirer

    //use raycast to shoot
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, range))
        {
            Debug.Log("Shooting : " + hit.transform.name);
            Ennemy ennemy = hit.transform.GetComponent<Ennemy>();
            if (ennemy)
            {
                ennemy.TakeDamage(damage);
            }
        }
    }


}
