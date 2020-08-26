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
        address = 1;
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

    // @param: red , green and blue values will all be in range ____
    // TODO: is it ok to give colour ranged 0-255 or would it be better to have 0-1 or something else??
    public void setColour(int r, int g, int b){
        // TODO: if statements to catch if r,g,b vals are <0 or >255 and then to print notif and set to respective min/max

        // TODO
    }


}
