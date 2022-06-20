using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using Lean.Touch;


public class ARTapToPlace : MonoBehaviour
{
    //GameManager GM;
    public ARRaycastManager arOrigin;
    Pose placementPose;
    public bool validPose;
    public bool trackSelected;
    public GameObject placementIndicator;
    public GameObject[] objectToPlace;
    GameObject spawnedObject;
    public bool objectGenerated;
    public int trackChosen;
    public bool trackAdjusted;
    
    void Start()
    {
        placementIndicator.SetActive(false);
    }

    
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (spawnedObject == null && validPose && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && trackSelected)
        {
            PlaceObject();
        }
    }

    void PlaceObject()
    {
        spawnedObject = Instantiate(objectToPlace[trackChosen], placementPose.position, placementPose.rotation);
        placementIndicator.SetActive(false);
        objectGenerated = true;
    }
    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && validPose && trackSelected)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }

        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCentre = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCentre, hits, TrackableType.Planes);

        validPose = hits.Count > 0; //there is a valid position to place the object

        if (validPose)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

        }
    }

    public void TrackAdjusted()
    {
        trackAdjusted = true;

        spawnedObject.GetComponent<LeanDragTranslate>().enabled = false;
        spawnedObject.GetComponent<LeanPinchScale>().enabled = false;
        spawnedObject.GetComponent<LeanTwistRotateAxis>().enabled = false;

    }
}
