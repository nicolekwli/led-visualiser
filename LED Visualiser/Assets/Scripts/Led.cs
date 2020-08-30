using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led : MonoBehaviour
{   
    
    public string label;
    public int address;
    public int universe;// NOTE: irl, universe is set on the artnet node, but we don't need do that...right?

    private Renderer rend; 
    Color ledCol;
    Color receiveCol; // Can't update material colour outside of Update() so trying this



    // // Constructor method
    // public Led(int addr){
    //     address = addr;

    // }

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        plugIn();

        ledCol = new Color(0,0,0);
        receiveCol = new Color(0,0,0); // I think receiveCol = col would copy the reference to the new colour object?
    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.color = (Color.white/2.0f) *Time.deltaTime;
        if (Input.GetKeyDown("space")){
            rend.material.SetColor("_EmissionColor", Random.ColorHSV());
        }
        
        // I think will work because colours are just vec4s?
        if (ledCol != receiveCol) {
            Debug.Log("DMX changed");
            Debug.Log(receiveCol);
            setColour(receiveCol);
        }
        
    }

    void plugIn(){
        GameObject node = GameObject.Find("node");
        ArtNetNode a = node.GetComponent<ArtNetNode>(); //script
        a.plugInFixture(this);
    }

    // @param: red , green and blue values will all be in range 0,1
    public void setColour(Color c){
        receiveCol = c;
        ledCol = c;

        rend.material.SetColor("_EmissionColor", ledCol);
    }

    // TODO: Make parameter arbitrary length
    // NOTE: So the way colour is set is a bit weird. First the fixture receives DMX, it updates receiveCol. When Update realises that there's been a change in colour, it'll call setColour, and then the colour is changed.
    public void receiveDMX(int data1, int data2, int data3){

        receiveCol.r = (float) data1 / 255;
        receiveCol.g = (float) data2 / 255;
        receiveCol.b = (float) data3 / 255;

        
    }

}
