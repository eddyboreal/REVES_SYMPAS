using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNegative : MonoBehaviour
{
    bool test = false;
    float a = 0;
    float b = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!test)
        {
            a += Time.deltaTime;
            if(a <= 1)
            {
                Debug.Log("a");
                GetComponent<MeshRenderer>().material.SetFloat("_Threshold", a);
            }
            else 
            {
                test = true;
                b = 1;
            }
            
        }
        else
        {
            b -= Time.deltaTime;
            if (b >= 0)
            {
                Debug.Log("b");
                GetComponent<MeshRenderer>().material.SetFloat("_Threshold", b);
            }
            else
            {
                test = false;
                a = 0;
            }
        }
    }


}
