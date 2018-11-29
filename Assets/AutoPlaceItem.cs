using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARSessionOrigin))]
public class AutoPlaceItem : MonoBehaviour
{

    [SerializeField]
    //[Tooltip("Instantiates this prefab on a plane at the touch Location.")]
    GameObject GameObjectToPlace;


    ARSessionOrigin m_SessionOrigin;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
    }


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