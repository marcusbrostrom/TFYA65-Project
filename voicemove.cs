using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class voicemove : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {

        actions.Add("left", Left);
        actions.Add("right", Right);
        Debug.Log("hej");
        print("h");
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        findMicrophones();

    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text + "tja");
        Debug.Log("hej");
        actions[speech.text].Invoke();

    }
    private void Left()
    {

        if (transform.position.x > -2.5f)
        {
            transform.Translate(-2.5f, 0, 0);
        }
    }
    private void Right()
    {
        if (transform.position.x < 2.5f)
        {
            transform.Translate(2.5f, 0, 0);
        }
    }

    void findMicrophones()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
    }

}

