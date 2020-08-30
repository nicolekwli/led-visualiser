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
        // TODO: generate LED strip, not just a single LED
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






    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
