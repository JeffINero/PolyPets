using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

/**
 * Plaziert das Objekt auf die Oberfläche wenn darauf geklickt wird.
 */
[RequireComponent(typeof(ARSessionOrigin))]
public class PlaceOnPlan : MonoBehaviour {

    [SerializeField]
    GameObject m_PlacedPrefab;

    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    public GameObject playPanel;
    public GameObject spawnedObject { get; private set;}

    ARSessionOrigin m_SessionOrigin;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    /**
     * Wird aufgerufen beim inizialisieren.
     */
	void Awake () {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();  
    }
	
    /**
     * Überprüft, ob irgendwo aufdem Bildschirm geklickt wurde und platziert dort dann 
     * das Objekt.
     */
	void Update () {
        // Überprüfe, ob wo geklickt wurde.
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Überprüfe, ob der klick auf einem Feld war.
            if (playPanel.active&& m_SessionOrigin.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                
                Pose hitPose = s_Hits[0].pose;
                // Falls das Objekt noch nicht angezeigt ist dann...
                if(spawnedObject == null)
                {
                    // ... erstelle es und zeige es an.
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    // ... sonst bewege seine Position.
                    spawnedObject.transform.position = hitPose.position;
                }
                if (!spawnedObject.active)
                {
                    // Setzte das Objekt auf "Aktive".
                    spawnedObject.SetActive(true);
                }
            }
        }
		
	}
}
