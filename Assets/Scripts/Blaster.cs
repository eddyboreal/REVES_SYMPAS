using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Blaster : MonoBehaviour
{
    public int damage = 10;
    public float loadTime = 1.5f;

    public float reloadTime = 3f;
    public float elapsedReloadingTime = 0;


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

    int PlayerMask = 1 << 9;

    void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.25f, 0.25f);

        hits = new RaycastHit[reflections];

        PlayerMask = ~PlayerMask;
    }

    void Update()
    {
        RaycastReflection();
        LoadBlaster();

        /*Time.timeScale += (1f / 16) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);*/
    }

    void LoadBlaster()
    {
        shoot = false;

        if (Input.GetButtonDown("Fire1") && elapsedReloadingTime >= reloadTime)
        {
            Time.timeScale = 0.05f;
            Time.fixedDeltaTime = Time.timeScale * 0.2f;
        }

        if (Input.GetButton("Fire1") && elapsedReloadingTime >= reloadTime)
        {
            
            GetComponentInParent<MouseLook>().mouseSensitivity = 1000;
            elapsedLoadingTime += Time.deltaTime;

            loadingGauge.value = elapsedLoadingTime / loadTime;

            if (elapsedLoadingTime >= loadTime)
            {
                elapsedReloadingTime = 0f;
                loadingGauge.value = 0f;
                elapsedLoadingTime = 0f;
                shoot = true;
            }
        }
        else
        {
            Time.timeScale = 1f;
            GetComponentInParent<MouseLook>().mouseSensitivity = 300;
            loadingGauge.value = 0f;
            elapsedLoadingTime = 0f;
            elapsedReloadingTime += Time.deltaTime;
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
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, PlayerMask))
            {
                hits[i] = hit;
                ++lineRenderer.positionCount;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                Vector3 lastRayOrigin = ray.origin;

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                
                if (hit.collider.gameObject.CompareTag("Tile") && shoot)
                {
                    hit.collider.GetComponent<TileController>().touched = true;
                }
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
            newBullet.GetComponent<Bullet>().originCamPosition = fpsCam.transform.position;
            for (int i = 0; i < hits.Length; ++i)
            {
                newBullet.GetComponent<Bullet>().transforms[i] = hits[i].point;
            }
            //newBullet.GetComponent<Bullet>().SetHits(hits);
            shoot = false;
        }
    }

}
