﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GoogleMap : MonoBehaviour
{
    public enum MapType
    {
        RoadMap,
        Satellite,
        Terrain,
        Hybrid
    }

    public string GoogleApiKey;
    public bool loadOnStart = true;
    public bool autoLocateCenter = true;
    public GoogleMapLocation centerLocation;
    public int zoom = 13;
    public MapType mapType;
    public int size = 512;
    public bool doubleResolution = false;
    public GoogleMapMarker[] markers;
    public GoogleMapPath[] paths;
    public RawImage img;
    public InputField inpuStart;
    public InputField inputEnd;
    public Slider sliderZoom;
    public Slider sliderMoveMap;



    void Start()

    {
        sliderZoom.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sliderMoveMap.onValueChanged.AddListener(delegate { ValueChangeMove(); });

        if (loadOnStart)
            Refresh();
    }

    public void Refresh()
    {
        if (autoLocateCenter && (markers.Length == 0 && paths.Length == 0))
        {
            Debug.LogError("Auto Center will only work if paths or markers are used.");
        }

        StartCoroutine(_Refresh());
    }

    IEnumerator _Refresh()
    {
        string url = "http://maps.googleapis.com/maps/api/staticmap";
        string qs = "";
        if (!autoLocateCenter)
        {
            if (centerLocation.address != "")
                qs += "center=" + WWW.UnEscapeURL(centerLocation.address);
            else
                qs += "center=" + WWW.UnEscapeURL(string.Format("{0},{1}", centerLocation.latitude, centerLocation.longitude));

            qs += "&zoom=" + zoom.ToString();
        }
        qs += "&size=" + WWW.UnEscapeURL(string.Format("{0}x{0}", size));
        qs += "&scale=" + (doubleResolution ? "2" : "1");
        qs += "&maptype=" + mapType.ToString().ToLower();
        var usingSensor = false;

#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
#endif

        qs += "&sensor=" + (usingSensor ? "true" : "false");

        foreach (var i in markers)
        {
            qs += "&markers=" + string.Format("size:{0}|color:{1}|label:{2}", i.size.ToString().ToLower(), i.color, i.label);

            foreach (var loc in i.locations)
            {
                if (loc.address != "")
                    qs += "|" + WWW.UnEscapeURL(loc.address);
                else
                    qs += "|" + WWW.UnEscapeURL(string.Format("{0},{1}", loc.latitude, loc.longitude));
            }
        }

        foreach (var i in paths)
        {
            qs += "&path=" + string.Format("weight:{0}|color:{1}", i.weight, i.color);

            if (i.fill)
                qs += "|fillcolor:" + i.fillColor;

            foreach (var loc in i.locations)
            {
                if (loc.address != "")
                    qs += "|" + WWW.UnEscapeURL(loc.address);
                else
                    qs += "|" + WWW.UnEscapeURL(string.Format("{0},{1}", loc.latitude, loc.longitude));
            }
        }

        qs += "&key=" + WWW.UnEscapeURL(GoogleApiKey);

        WWW req = new WWW(url + "?" + qs);
        Debug.Log(url + "?" + qs);

        // Create a texture in DXT1 format
        img.texture = new Texture2D(size, size, TextureFormat.DXT1, false);

        while (!req.isDone)
            yield return null;
        if (req.error == null)
            req.LoadImageIntoTexture((Texture2D)img.texture);
    }

    public void runTheMap()
    {
        
        this.markers[0].locations[0].address = inpuStart.text;
        this.markers[0].locations[1].address = inputEnd.text;
        this.paths[0].locations[0].address = inpuStart.text;
        this.paths[0].locations[1].address = inputEnd.text;
        this.centerLocation.address = inpuStart.text;
        StartCoroutine(_Refresh());

    }
    public void ValueChangeCheck()
    {
        zoom = (int)sliderZoom.value;
        StartCoroutine(_Refresh());
    }

    public void ValueChangeMove()
    {
        if (sliderMoveMap.value > 3)
        {
            centerLocation.longitude += 0.01f;
            centerLocation.address = "";
        }else if (sliderMoveMap.value < 3)
        {
            centerLocation.longitude -= 0.01f;
            centerLocation.address = "";
        }
            StartCoroutine(_Refresh());
    }

}

public enum GoogleMapColor
{
    black,
    brown,
    green,
    purple,
    yellow,
    blue,
    gray,
    orange,
    red,
    white
}

[System.Serializable]
public class GoogleMapLocation
{
    public string address;
    public float latitude;
    public float longitude;
}

[System.Serializable]
public class GoogleMapMarker
{
    public enum GoogleMapMarkerSize
    {
        Tiny,
        Small,
        Mid
    }
    public GoogleMapMarkerSize size;
    public GoogleMapColor color;
    public string label;
    public GoogleMapLocation[] locations;

}

[System.Serializable]
public class GoogleMapPath
{
    public int weight = 5;
    public GoogleMapColor color;
    public bool fill = false;
    public GoogleMapColor fillColor;
    public GoogleMapLocation[] locations;
}


