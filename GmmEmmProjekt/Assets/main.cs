using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Leap;
using Leap.Unity;
using System;



public class main : MonoBehaviour
{

    LeapServiceProvider provider;
    public GameObject curser;
    private float handPosition;
    private float upperYBoundries;
    private float underYboundries;

    void Start()
    {
        upperYBoundries = 55f;
        underYboundries = -55f;

        provider = FindObjectOfType<LeapServiceProvider>() as LeapServiceProvider;
    }
    void Update()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            handPosition = hand.Direction.y;
            if (hand.IsRight)
            {
                
                if (curser.transform.position.y <= upperYBoundries && curser.transform.position.y >= underYboundries)
                {
                    if (handPosition > 0.5)
                    { 
                        curser.transform.position += new Vector3(0, 2, 0);
                    }
                    if (handPosition < -0.5)
                    {
                        curser.transform.position -= new Vector3(0, 2, 0);
                    }

                }
            }
        }
    }
}



