using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleApi : MonoBehaviour
{
    public InputField text;
    public RawImage img;

    string url;

    public static float lat= 52.5200066f;
    public static float lon = 13.404954f;

    LocationInfo li;

    public int zoom = 14;
    public int mapWidth = 640;
    public int mapHeight = 640;

    public enum mapType { roadmap, satellite, hybrid, terrain }
    public mapType mapSelected;
    public int scale;
    private float z;
    private float z1; 

    IEnumerator Map()
    {

        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon + "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale
            + "&maptype=" + mapSelected +
            "&markers=color:blue%7Clabel:S%7C" + lat + "," + lon + "&markers=color:red%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=AIzaSyDYVAWfzQZfRs91zQ0cXx0iPl5JOpj_Rmw";
        #pragma warning disable CS0618 // Type or member is obsolete
        WWW www = new WWW(url);
        #pragma warning restore CS0618 // Type or member is obsolete
        yield return www;
        img.texture = www.texture;
        img.SetNativeSize();

    }
    // Use this for initialization
    void Start()
    {
        z = 0;
        z1 = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (z != GoogleApi.lat && z1 !=GoogleApi.lon)
        {
            img = gameObject.GetComponent<RawImage>();
            StartCoroutine(Map());
            z = GoogleApi.lat;
            z1 = GoogleApi.lon;
        }
    }
    
    
}
