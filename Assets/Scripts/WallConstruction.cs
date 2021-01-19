using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WallConstruction : MonoBehaviour
{
    public GameObject cube;
    public int NumberOfXCubes;
    public int NumberOfYCubes;
    public int NumberOfZCubes;
    public float cubeStep;
    public float cubeScale;

    private float xSpawn;
    private float ySpawn;
    private float zSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NumberOfXCubes + NumberOfYCubes != gameObject.transform.childCount && Application.isEditor)
        {
            for (int i = this.transform.childCount; i > 0; --i)
            {
                DestroyImmediate(this.transform.GetChild(0).gameObject);
            }

            GameObject a = cube;
            a.transform.localScale = new Vector3(cubeScale, cubeScale, cubeScale);

            zSpawn = -(NumberOfZCubes * cubeStep / 2);
            for (int h = 0; h < NumberOfZCubes; h++)
            {
                ySpawn = (cubeStep / 2);
                for (int i = 0; i < NumberOfYCubes; i++)
                {
                    xSpawn = -(NumberOfXCubes * cubeStep / 2) + cubeScale/2;
                    for (int j = 0; j < NumberOfXCubes; j++)
                    {
                        Vector3 b = new Vector3(xSpawn, ySpawn, zSpawn) + transform.position;
                        Instantiate(cube, b, new Quaternion(0, 0, 0, 0), gameObject.transform);
                        xSpawn += cubeStep;
                    }
                    ySpawn += cubeStep;
                }
                zSpawn += cubeStep;
            }
        }
    }
}
