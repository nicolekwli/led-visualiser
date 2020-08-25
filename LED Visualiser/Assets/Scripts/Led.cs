using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour
{
    private Renderer rend; 
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.color = (Color.white/2.0f) *Time.deltaTime;
        if (Input.GetKeyDown("space")){
            rend.material.SetColor("_EmissionColor", Random.ColorHSV());
        }
            
        
    }


}
