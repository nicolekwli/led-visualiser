using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.D)) {
            transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Rotate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Rotate(new Vector3(-speed * Time.deltaTime, 0 , 0));
        }
    }   
}