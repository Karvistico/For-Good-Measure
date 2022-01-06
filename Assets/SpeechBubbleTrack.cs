using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleTrack : MonoBehaviour
{
    public Transform target;
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            transform.position = cam.WorldToScreenPoint(target.position);
    }
}
