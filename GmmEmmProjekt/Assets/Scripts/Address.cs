using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Address : MonoBehaviour
{
    public InputField text;

    IEnumerator geoData()
    {
        string address = text.text;

        string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=AIzaSyDYVAWfzQZfRs91zQ0cXx0iPl5JOpj_Rmw";
        // Start a download of the given URL
        using (WWW www = new WWW(url))
        {
            // Wait for download to complete
            yield return www;

            // assign texture
            Debug.Log(www.text);
            string lat = getLat(www.text);
            string lng = getLng(www.text);
            float latf = float.Parse(lat, System.Globalization.CultureInfo.InvariantCulture);
            float lonf = float.Parse(lng, System.Globalization.CultureInfo.InvariantCulture);
            GoogleApi.lon = lonf;
            GoogleApi.lat = latf;

            Debug.Log(GoogleApi.lon);
            Debug.Log(GoogleApi.lat);
            

        }
    }


    public void getTheLocation()
    {
        StartCoroutine(geoData());
    }

    string getLat(string str)
    {
        string strStart = "\"lat\" : ";
        string strEnd = ",";
        int Start, End;
        if (str.Contains(strStart) && str.Contains(strEnd))
        {
            Start = str.IndexOf(strStart, 0) + strStart.Length;
            End = str.IndexOf(strEnd, Start);
            string s = str.Substring(Start, End - Start);
            return s;
        }
        else
        {
            return null;
        }
    }

    string getLng(string str)
    {
        string strStart = "\"lng\" : ";
        string strEnd = "}";
        int Start, End;
        if (str.Contains(strStart) && str.Contains(strEnd))
        {
            Start = str.IndexOf(strStart, 0) + strStart.Length;
            End = str.IndexOf(strEnd, Start);
            string s = str.Substring(Start, End - Start);
            return s;
        }
        else
        {
            return null;
        }
    }
}


