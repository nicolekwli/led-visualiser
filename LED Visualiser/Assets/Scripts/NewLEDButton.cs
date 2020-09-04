using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; // Need this for Int32, how to get just Int32 from it and not the whole library?

public class NewLEDButton : MonoBehaviour
{

    public Material mat; //variable set in inspector
    public InputField newAddressField;


    public void newLEDStrip () {
        // int stripLen = 1; // 1m
        int numLEDs = 30;
        float stripLen = 1.5f; //temp until we set world dimensions

        GameObject strip = GameObject.CreatePrimitive(PrimitiveType.Cube);
        strip.transform.localScale = new Vector3(stripLen,0.008f,0.025f);


        /***  generate individual cells on the strip  ***/
        
        GameObject[] leds = new GameObject[numLEDs]; 
        // TODO: move all this to an initLED function?
        float w = 0.005f;
        Vector3 ledDim = new Vector3(w, 0.001f, w);
        // stripLen = 30*w + 30*gap, edge = gap/2
        float gap = 0.045f; // TODO: this should be dependant on numLEDs
        float edge = 0.00225f;    
        // This for loop could very nicely be parrallelised
        for (int i = 0; i < numLEDs; i++){
            leds[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // TODO: parent all these gameobjects to strip and give each unique identifier (like LED-i)
            leds[i].transform.localScale = ledDim; //  does this assign by val or assign ref? maybe do localScale *= ledDim?
            float x = edge +  i * (gap+w)  -stripLen/2 ;
            leds[i].transform.position = new Vector3(x, 0.001f, 0.0f);
        }

        // TODO: parent the LEDs to the strip

        /***  address cells on the strip  ***/
        // TODO: basically copy the bit below abuot the text field to get the first address, then every pixels address after will be prev+3

    }

    public void newLED(){
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(10,3,2);
        cube.GetComponent<MeshRenderer>().material = mat;
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


