using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{

    public bool touched = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (touched)
        {
            Helper helper = GameObject.FindGameObjectWithTag("Helper").GetComponent<Helper>();
            helper.FadeColor(
                        gameObject.GetComponent<MeshRenderer>(),
                        Color.black,
                        Color.white,
                        Color.white,
                        Color.black,
                        helper.TileFadeInDuration,
                        helper.TileFadeOutDuration,
                        true
                    );
            touched = false;
        }
    }

}
