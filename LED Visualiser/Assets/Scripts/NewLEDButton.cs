using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Need this for Int32, how to get just Int32 from it and not the whole library?

public class NewLEDButton : MonoBehaviour
{
    // Variables set in inspector
    public Material ledmat; 
    public Material stripMat;
    public InputField newAddressField;
    public GameObject camera;


    public void newLEDStrip () {
        // int stripLen = 1; // 1m
        int numLEDs = 30;
        float stripLen = 1.5f; //temp until we set world dimensions
        string addrStr = newAddressField.text;

        GameObject strip = GameObject.CreatePrimitive(PrimitiveType.Cube);
        strip.transform.localScale = new Vector3(stripLen,0.008f,0.025f);
        strip.name = "LEDStrip-"; // use addrStr
        strip.GetComponent<MeshRenderer>().material = stripMat;


        /***  generate individual cells on the strip  ***/
        
        GameObject[] leds = new GameObject[numLEDs]; 
        // TODO: move all this to an initLED function?
        float w = 0.005f;
        Vector3 ledDim = new Vector3(w, 0.01f, w);
        // Must satisfy: stripLen = 30*w + 30*gap, edge = gap/2
        float gap = 0.045f; // TODO: this should be dependant on numLEDs?
        float edge = 0.0225f;    

        // This for loop could very nicely be parrallelised
        for (int i = 0; i < numLEDs; i++){
            leds[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leds[i].transform.localScale = ledDim; //  does this assign by val or assign ref? maybe do localScale *= ledDim?
            float x = edge +  i * (gap+w)  -stripLen/2 ;
            leds[i].transform.position = new Vector3(x, 0.001f, 0.0f);
            leds[i].name = "LED-" + i.ToString();
        }

        foreach (GameObject led in leds){
            led.transform.SetParent(strip.transform);
        }

        /***  address cells on the strip  ***/
        // ledGO = GameObject
        // ledS  = Script
        int startAddr;
        if (Int32.TryParse(addrStr, out startAddr)) {
            for (int i =0; i < numLEDs; i++) {
                GameObject ledGO = leds[i];
                ledGO.GetComponent<MeshRenderer>().material = ledmat;
                Led ledS = ledGO.AddComponent(typeof(Led)) as Led;
                ledS.address = (startAddr + i*3) % 512; // TODO: universes, test for case 512 and 0 as these will mess up
            }
        } else { // int not entered in the text box
            // TODO
            Debug.Log("Text Box Int Parse Failed");
        }


        /*** Move strip to be in front of camera ***/
        // Assumes camera is pointing straight down z-axis as it does at start
        Vector3 displacement = new Vector3(0.0f,-3.0f,-10.0f); // Is displacement the right word?
        strip.transform.position = camera.transform.position + displacement;



    }

    public void newLED(){
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(10,3,2);
        cube.GetComponent<MeshRenderer>().material = ledmat;
        Led led = cube.AddComponent(typeof(Led)) as Led;
        string addrStr = newAddressField.text;

        if (Int32.TryParse(addrStr, out int addr)){ // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number
            // TODO: make sure address in range 1-512
            led.address = addr;
        } else {
            // TODO
        }
        
    }




}


