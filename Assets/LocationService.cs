using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * LocationService Singleton
 */
public class LocationService : MonoBehaviour
{
    private float lat;
    private float lon;

    public float oldDistance;

    private static LocationService ls;

    private LocationService()
    {
    }

    public static LocationService getInstance()
    {
        if (LocationService.ls == null )
        {
            LocationService.ls = new LocationService();
        }

        return LocationService.ls;
    }

    public void begin()
    {
        // Start();
    }

    /**
     * Start Location Service.
     */
    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;

            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        Input.location.Stop();
    }

    /**
     * Updated den Text von dem zurück gelegtem weg.
     */
    public void updateLocation(object state)
    {
        getDistanceTraveled();
    }

    /**
     * Holt die gesamte distance bewegt.
     */
    private string getDistanceTraveled()
    {
        oldDistance += distanceInMeters(lat, lon, getLat(), getLon());

        return oldDistance.ToString("0.0");
    }

    /**
     * 
     */
    private float distanceInMeters(float lat, float lon, float newLat, float newLon) 
    {
        float newDis = newLat - lat;
        float newDis2 = newLon - lon;
        float dis = newDis * newDis2 / 20;

        return 0.1F;
    }

    private float getLat()
    {
        //return Input.location.lastData.latitude;
        return 0;
    }

    private float getLon()
    {
        //return Input.location.lastData.longitue;
        return 0;
    }
}