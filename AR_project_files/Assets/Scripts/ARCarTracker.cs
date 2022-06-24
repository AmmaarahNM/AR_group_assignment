using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System.Collections.Generic;

public class ARCarTracker : MonoBehaviour
{
    private ARTrackedImageManager _trackedImageManager;
    //Used to determine which exact image is being tracked to match prefab
    private string _imageName;

    GameManager GameManager;
    //The non-ar camera
    [SerializeField] Camera _normalCam;
    [SerializeField] Camera _arCam;

    [SerializeField] GameObject _trackingSphere;
    [SerializeField] GameObject _staticGameObjects;

    [SerializeField] List<GameObject> _carGameObjects = new List<GameObject>();

    private void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        GameManager = FindObjectOfType<GameManager>();
        _trackingSphere.SetActive(false);
    }

    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= ImageChanged;
    }
    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackingMarker(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackingMarker(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            _trackingSphere.SetActive(false);

            foreach(GameObject car in _carGameObjects)
            {
                car.SetActive(false);
            }
        }
    }

    private void UpdateTrackingMarker(ARTrackedImage trackedImage)
    {
        _imageName = trackedImage.referenceImage.name;
        Vector3 trackPosition = trackedImage.transform.position;
        Quaternion trackRotation = trackedImage.transform.rotation;

        _trackingSphere.transform.position = trackPosition;
        _trackingSphere.transform.rotation = trackRotation;
        _trackingSphere.SetActive(true);

        UpdateCarMovement(_imageName);
    }

    private void UpdateCarMovement(string imageName)
    {
        _staticGameObjects.transform.position = _trackingSphere.transform.position;
        _staticGameObjects.transform.rotation = _trackingSphere.transform.rotation;

        if(_imageName == "")
        {
            //GameManager.carSpawned = false;
        }
        else
        {
            _carGameObjects[0].SetActive(true);
            GameManager.carSpawned = true;
        }
    }
}
