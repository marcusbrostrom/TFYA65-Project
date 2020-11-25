using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveonaudio : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnThreshold = 0.5f;
    public float spawnThreshold2 = 0.5f;
    public float spawnThreshold3 = 0.5f;
    public float radius = 0.5f;
    public FFTWindow fftWindow;
    public float debugValue1, debugValue2, debugValue3;
    public float debugValue;
    public bool validPosition,validPosition2,validPosition1;
    public float frames = 0;
    private float[] samples = new float[1024]; //MUST BE A POWER OF TWO
    public GameObject cube,cube2,cube3;
    public float refValue = 0.1f;
    public float db1, db2, db3, rms1, rms2, rms3;
    void Awake()
    {


        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(2, 0, 20);
        cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube2.transform.position = new Vector3(0, 0, 20);
        cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube3.transform.position = new Vector3(-2, 0, 20);


    }

    // Update is called once per frame
    void Update()
    {
     
        Frame10Update();
        frames++;
        if (frames % 100 == 0)
        { //If the remainder of the current frame divided by 10 is 0 run the function.
            
        }

    }

    void Frame10Update() {
        AudioListener.GetSpectrumData(samples, 0, fftWindow);


        for (int i = 0; i < 10; i++)
        {
            debugValue3 += samples[i];
        }
        for (int i = 10; i < 100; i++)
        {
                debugValue2 += samples[i];
        }
        for (int i = 100; i < 400; i++)
        {
                debugValue1 += samples[i];
        }
        //debugValue1 = Mathf.Sqrt(debugValue1/10); // rms = square root of average
        //debugValue1 = 20 * Mathf.Log10(rms1/refValue); // calculate dB

        //debugValue2 = Mathf.Sqrt(debugValue2/90); // rms = square root of average
        //debugValue2 = 20 * Mathf.Log10(rms2/refValue); // calculate dB

        //debugValue3 = Mathf.Sqrt(debugValue3/300); // rms = square root of average
        //debugValue3 = 20 * Mathf.Log10(rms3/refValue); // calculate dB

        cube.transform.position = new Vector3(2.5f, 5*debugValue1 * 0.5f - 0.5f+0.5f, 30);
        cube.transform.localScale = new Vector3(1, 5*debugValue1, 1);
        cube.GetComponent<Renderer>().material.color = new Vector4(debugValue1/2, 0, 1-debugValue1/2, 0);


        cube2.transform.position = new Vector3(0, 5 * debugValue2 * 0.5f - 0.5f + 0.5f, 30);
        cube2.transform.localScale = new Vector3(1, 5 * debugValue2, 1);
        cube2.GetComponent<Renderer>().material.color = new Vector4(Mathf.Abs(debugValue2), 1, 0.5f,0 );

        cube3.transform.position = new Vector3(-2.5f, 5 * debugValue3 * 0.5f - 0.5f + 0.5f, 30);
        cube3.transform.localScale = new Vector3(1, 5 * debugValue3, 1);
        cube3.GetComponent<Renderer>().material.color = new Vector4(1-debugValue3/2, 0, 0, 1-debugValue3/2);


        if (debugValue1 > spawnThreshold)
        {
            validPosition = true;
            // Collect all colliders within our Obstacle Check Radius
            if (Physics.CheckBox(transform.position, new Vector3(1, 1, radius)))
            {
                debugValue = 5*debugValue1;
                validPosition = false;
            }

            if (validPosition)
            {
               
                Destroy(Instantiate(objectPrefab, transform.position, transform.rotation), 25);
            }
        }
        if (debugValue2 > spawnThreshold2)
        {

            Vector3 pos1 = transform.position + new Vector3(-2.5f, 0, 0);
            validPosition1 = true;
            // Collect all colliders within our Obstacle Check Radius
            if (Physics.CheckBox(pos1, new Vector3(1, 1, radius)))
            {
                debugValue = debugValue1;
                validPosition1 = false;
            }

            if (validPosition1)
            {
                Destroy(Instantiate(objectPrefab, pos1, transform.rotation), 25);
            }
  

        }
        if (debugValue3 > spawnThreshold3)
        {
            Vector3 pos = transform.position + new Vector3(-5f, 0, 0);
            validPosition2 = true;
            // Collect all colliders within our Obstacle Check Radius
            if (Physics.CheckBox(pos, new Vector3(1,1,radius)))
            {
                debugValue = debugValue1;
                validPosition2 = false;
            }

            if (validPosition2)
            {
                Destroy(Instantiate(objectPrefab, pos, transform.rotation), 25);
            }

        }
        debugValue1 = 0;
        debugValue2 = 0;
        debugValue3 = 0;
    }
}
