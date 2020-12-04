using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;

    [SerializeField, Range(0, 1f)]
    private float fire_cooldown = 1f;
    private float fire_cooldown_counter = 1f;
    private bool is_allowed_to_fire = true;

    public GameObject LaserEmitter;
    private ProjectileEmitter projectileEmitter;

    public Camera FPSCam;

    void Start()
    {
        projectileEmitter = LaserEmitter.GetComponent<ProjectileEmitter>();
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            fire_cooldown_counter -= Time.deltaTime;
            if (is_allowed_to_fire)
            {
                projectileEmitter.DrawLaser(FPSCam.transform.position + FPSCam.transform.forward * 0.75f, 
                    FPSCam.transform.forward, 
                    projectileEmitter.max_reflection_count);
            }
            else
            {
                projectileEmitter.resetLaserLines();
            }
            if (fire_cooldown_counter <= 0 && is_allowed_to_fire)
            {
                Shoot();
                fire_cooldown_counter = fire_cooldown;
                is_allowed_to_fire = false;
            }
        }
        else
        {
            fire_cooldown_counter = fire_cooldown;
            is_allowed_to_fire = true;
            projectileEmitter.resetLaserLines();
        }
    }

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
