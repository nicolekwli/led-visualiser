using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFadeOut : MonoBehaviour
{

    Light lt;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lt.color -= (Color.white/2.0f) *Time.deltaTime;
    }
}
