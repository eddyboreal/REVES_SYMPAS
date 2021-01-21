using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Blaster : MonoBehaviour
{
    public int damage = 10;
    public float loadTime = 1.5f;
    public float TileFadeInDuration;
    public float TileFadeOutDuration;

    private float elapsedLoadingTime = 0f;
    public Slider loadingGauge = default;
    public Camera fpsCam;
    public Helper Helper;

    //Laser Management
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;

    private RaycastHit[] hits;
    private bool shoot = false;

    public Transform FireStart;

    public GameObject Bullet;
    public float speed = 50f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.25f, 0.25f);

        hits = new RaycastHit[reflections];
    }

    void Update()
    {
        RaycastReflection();
        LoadBlaster();
    }

    void LoadBlaster()
    {
        shoot = false;
        if (Input.GetButton("Fire1"))
        {
            elapsedLoadingTime += Time.deltaTime;

            loadingGauge.value = elapsedLoadingTime / loadTime;

            if (elapsedLoadingTime >= loadTime)
            {
                loadingGauge.value = 0f;
                elapsedLoadingTime = 0f;
                shoot = true;
            }
        }
        else
        {
            loadingGauge.value = 0f;
            elapsedLoadingTime = 0f;
        }
    }

    void RaycastReflection()
    {
        ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, fpsCam.transform.position);

        float remainingLength = maxLength;

        for (int i = 0; i < reflections; ++i)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                hits[i] = hit;
                ++lineRenderer.positionCount;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                
                if (hit.collider.gameObject.CompareTag("Tile") && shoot)
                {
                    Helper.FadeColor(
                        hit.collider.gameObject.GetComponent<MeshRenderer>(), 
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color, 
                        Helper.colors.possibleColors[Random.Range(0, Helper.colors.possibleColors.Length - 1)],
                        TileFadeInDuration,
                        TileFadeOutDuration
                    );
                }

                /*if (hit.transform.GetComponent<Ennemy>() && shoot)
                {
                    hit.transform.GetComponent<Ennemy>().TakeDamage(damage * (i+1), hit.point, ray.origin);
                    shoot = false;
                }*/
            }
            else
            {
                ++lineRenderer.positionCount;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
        if (shoot)
        {
            GameObject newBullet = Instantiate(Bullet, FireStart.position, FireStart.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().transforms = new Vector3[hits.Length];
            for (int i = 0; i < hits.Length; ++i)
            {
                newBullet.GetComponent<Bullet>().transforms[i] = hits[i].point;
            }
            //newBullet.GetComponent<Bullet>().SetHits(hits);
            shoot = false;
        }
    }

}
