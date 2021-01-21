using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour
{

    public List<Collider> colliderList = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(colliderList.Count == GetComponent<MeshCollider>().tri)
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!colliderList.Contains(other))
        {
            colliderList.Add(other);
        }
    }


}
