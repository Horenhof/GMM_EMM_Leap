using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Leap;
using Leap.Unity;
using System;



public class MainMenu : MonoBehaviour {

    LeapServiceProvider provider;
    public GameObject curser;
    private float handPosition;
    private float upperYBoundries;
    private float underYboundries;
    
    void Start()
    {
        upperYBoundries = 580 ;
        underYboundries = -580f;
        
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
                Debug.Log(curser.transform.position.y);
                if (curser.transform.position.y <=upperYBoundries && curser.transform.position.y>=underYboundries) {

                    
                if (handPosition > 0.5)
                {
                    Debug.Log("more");
                    curser.transform.position += new Vector3(0, 1, 0);
                }
                if (handPosition < -0.5)
                {
                    curser.transform.position -= new Vector3(0, 1, 0);
                }

                }
                else
                {
                    Debug.Log("s");
                    curser.transform.position = new Vector3(curser.transform.position.x,
                                                            upperYBoundries,
                                                            0);
                }
            }


        }
    }
}

    

