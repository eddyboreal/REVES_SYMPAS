using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour
{

    public List<Collider> colliderList = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        
        if (!colliderList.Contains(other) && other.gameObject.CompareTag("Ennemy"))
        {
            colliderList.Add(other);
        }
    }


}
