using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour
{   
    
    public string label { get; set; }
    public int address { get; set; }
    public int universe { get; set; }// NOTE: irl, universe is set on the artnet node, but we don't need do that...right?

    private Renderer rend; 



    // Constructor method
    public Led(){

    }

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        plugIn();
    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.color = (Color.white/2.0f) *Time.deltaTime;
        if (Input.GetKeyDown("space")){
            rend.material.SetColor("_EmissionColor", Random.ColorHSV());
        }
            
        
    }

    void plugIn(){
        GameObject node = GameObject.Find("node");
        ArtNetNode a = node.GetComponent<ArtNetNode>(); //script
        a.plugInFixture(this);
    }


}
