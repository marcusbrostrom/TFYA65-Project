using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveroad : MonoBehaviour
{

    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, Time.time/speed);
    }
}
