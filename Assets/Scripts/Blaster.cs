using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Blaster : MonoBehaviour
{
    public int damage = 10;
    public float loadTime = 1.5f;

    public float reloadTime = 3f;
    public float elapsedReloadingTime = 0;

    public float elapsedLoadingTime = 0f;
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

    public bool FireButtonPushed = false;

    int raycastIgnoredLayers = ~( (1 << 9) | (1 << 10) | (1 << 11));        // Ignores Layer 9 and 11

    public Canvas BulletTimeCanvas = default;
    private bool bulletTimeIn = true;

    public Text BounceText = default;

    public bool CanReceiveInputs = true;
    public Timer timer = default;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.25f, 0.25f);

        hits = new RaycastHit[reflections];
    }

    void Update()
    {
        if (CanReceiveInputs && timer.CanStart)
        {
            LoadBlaster();
            BulletTimeFade();
        }
        else
        {
            Debug.Log("HERE");
            lineRenderer.positionCount = 0;
        }

        /*Time.timeScale += (1f / 16) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);*/
    }

    void LoadBlaster()
    {
        shoot = false;

        if (Input.GetButtonDown("Fire1") || Input.GetAxisRaw("Fire1") >= 0.2f)
        {
            Debug.Log(Input.GetAxisRaw("Fire1"));
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        if (Input.GetButton("Fire1") || Input.GetAxisRaw("Fire1") >= 0.2f /*&& FireButtonPushed && elapsedReloadingTime >= reloadTime*/)
        {
            
            GetComponentInParent<MouseLook>().mouseSensitivity = 1000f;
            elapsedLoadingTime += Time.unscaledDeltaTime;

            loadingGauge.value = elapsedLoadingTime / loadTime;

            if (elapsedLoadingTime >= loadTime)
            {
                FireButtonPushed = false;
                elapsedReloadingTime = 0f;
                loadingGauge.value = 0f;
                elapsedLoadingTime = 0f;
                shoot = true;
            }
            RaycastReflection();
        }
        else
        {
            BounceText.text = "0";
            BounceText.color = Color.white;
            ResetRay();
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            GetComponentInParent<MouseLook>().mouseSensitivity = 50f;
            loadingGauge.value = 0f;
            elapsedLoadingTime = 0f;
            elapsedReloadingTime += Time.deltaTime;
        }
    }

    void RaycastReflection()
    {
        bool ennemyEncountered = false;
        int bounces = 0;

        ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, fpsCam.transform.position);
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        float remainingLength = maxLength;

        for (int i = 0; i < reflections; ++i)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, raycastIgnoredLayers))
            {
                ++bounces;
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
                if (hit.collider.gameObject.CompareTag("Ennemy"))
                {
                    lineRenderer.startColor = Color.green;
                    lineRenderer.endColor = Color.green;
                    ennemyEncountered = true;
                    break;
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
                if (hits[i].collider.gameObject.CompareTag("Ennemy") && newBullet.GetComponent<Bullet>().EnnemyHitIndex == -1)
                {
                    newBullet.GetComponent<Bullet>().EnnemyHitIndex = i;
                    newBullet.GetComponent<Bullet>().Ennemy = hits[i].collider.gameObject;
                }
            }
            //newBullet.GetComponent<Bullet>().SetHits(hits);
            shoot = false;
        }

        BounceText.text = bounces.ToString();

        if (ennemyEncountered)
        {
            BounceText.color = Color.green;
        }
        else if (!ennemyEncountered)
        {
            BounceText.color = Color.red;
        }
    }

    void ResetRay()
    {
        lineRenderer.positionCount = 0;
    }
    
    private void BulletTimeFade()
    {
        if (Input.GetButton("Fire1") || Input.GetAxisRaw("Fire1") >= 0.2f)
        {
            if (bulletTimeIn)
            {
                StopAllCoroutines();
                StartCoroutine(FadeCanvas(BulletTimeCanvas.GetComponent<CanvasGroup>(), BulletTimeCanvas.GetComponent<CanvasGroup>().alpha, 1f, 0.025f, true));
                bulletTimeIn = false;
            }
        }
        else if (Input.GetButtonUp("Fire1") || Input.GetAxisRaw("Fire1") < 0.2f)
        {
            if (!bulletTimeIn)
            {
                StopAllCoroutines();
                StartCoroutine(FadeCanvas(BulletTimeCanvas.GetComponent<CanvasGroup>(), BulletTimeCanvas.GetComponent<CanvasGroup>().alpha, 0f, 0.25f, true));
                bulletTimeIn = true;
            }
        }
    }

    public static IEnumerator FadeCanvas(CanvasGroup canvas, float startAlpha, float endAlpha, float duration, bool activate)
    {
        var startTime = Time.time;
        var endTime = Time.time + duration;
        var elapsedTime = 0f;

        canvas.alpha = startAlpha;

        if (activate)
        {
            canvas.gameObject.SetActive(true);
        }

        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime;
            var percentage = 1 / (duration / elapsedTime);
            if (startAlpha > endAlpha)
            {
                canvas.alpha = startAlpha - percentage;
            }
            else
            {
                canvas.alpha = startAlpha + percentage;
            }
            yield return new WaitForEndOfFrame();
        }

        canvas.alpha = endAlpha;
        if (!activate)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    public void ResetBulletTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        BulletTimeCanvas.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
