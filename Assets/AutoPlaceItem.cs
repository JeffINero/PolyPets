using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

/**
 * Platziert das Objekt automatisch an einer Stelle.
 */
[RequireComponent(typeof(ARSessionOrigin))]
public class AutoPlaceItem : MonoBehaviour
{
    [SerializeField]
    GameObject GameObjectToPlace;

    ARSessionOrigin m_SessionOrigin;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    /**
     * Wird aufgerufen beim inizialisieren.
     */
    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
    }

    /**
     * Platziert dass Objekt automisch in die Mitte des Bildschirmes, falls es noch nicht angezeigt wird
     * und eine Oberfläche vorhanden ist um diese Anzuzeigen.
     */
    void Update()
    {

        if (!GameObjectToPlace.active&&m_SessionOrigin.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = s_Hits[0].pose;
            GameObjectToPlace.transform.position = hitPose.position;
            GameObjectToPlace.SetActive(true);
        }
       

    }
}