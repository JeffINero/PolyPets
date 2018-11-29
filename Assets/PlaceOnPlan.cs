using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARSessionOrigin))]
public class PlaceOnPlan : MonoBehaviour {

    [SerializeField]
    //[Tooltip("Instantiates this prefab on a plane at the touch Location.")]
    GameObject m_PlacedPrefab;

    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }


    public GameObject spawnedObject { get; private set;}

    ARSessionOrigin m_SessionOrigin;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

	void Awake () {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();  
    }
	

	void Update () {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (m_SessionOrigin.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)), s_Hits, TrackableType.PlaneWithinPolygon))
            {
                
                Pose hitPose = s_Hits[0].pose;
                if(spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                }
                if (!spawnedObject.active)
                {
                    spawnedObject.SetActive(true);
                }
            }
        }
		
	}
}
