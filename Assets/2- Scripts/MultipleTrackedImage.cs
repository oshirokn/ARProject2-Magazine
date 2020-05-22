
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
 
[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleTrackedImage : MonoBehaviour
{
    [Header("The length of this list must match the number of images in Reference Image Library")]
    public List<GameObject> ObjectsToPlace;

    private int refImageCount;
    private Dictionary<string, GameObject> allObjects;
    private ARTrackedImageManager arTrackedImageManager;
    private XRReferenceImageLibrary refLibrary;


    [SerializeField] TMPro.TMP_Text StateText;
    [SerializeField] TMPro.TMP_Text _debugText;

    ARTrackedImage trackedImage;

    void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;

        ARSession.stateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void Start()
    {
        refLibrary = arTrackedImageManager.referenceLibrary;
        refImageCount = refLibrary.count;
        LoadObjectDictionary();
    }

    void LoadObjectDictionary()
    {
        allObjects = new Dictionary<string, GameObject>();
        for (int i = 0; i < refImageCount; i++)
        {
            allObjects.Add(refLibrary[i].name, ObjectsToPlace[i]);
            ObjectsToPlace[i].SetActive(false);
        }
    }

    void ActivateTrackedObject(string _imageName)
    {
        _debugText.text = "Object spawned " + allObjects[_imageName];
        allObjects[_imageName].SetActive(true);
    }

    
    void DisableTrackedObject(string _imageName)
    {
        _debugText.text = "Removed Object" + allObjects[_imageName];
        allObjects[_imageName].SetActive(false);
    }
    


    
    public void OnImageChanged(ARTrackedImagesChangedEventArgs _args)
    {
       
        /*Not supported
        foreach (var addedImage in _args.added)
        {
            ActivateTrackedObject(addedImage.referenceImage.name);
        }

        
        //args.removed DOES NOT WORK see https://github.com/Unity-Technologies/arfoundation-samples/issues/261
        foreach (var addedImage in _args.removed)
        {
            DisableTrackedObject(addedImage.referenceImage.name);
        }
        */

        foreach (var updated in _args.updated)
        {
            
                _debugText.text = "Tracking Image";
                allObjects[updated.referenceImage.name].transform.position = updated.transform.position;
                //allObjects[updated.referenceImage.name].transform.rotation = updated.transform.rotation;

            if (updated.trackingState == TrackingState.Tracking)
            {
                ActivateTrackedObject(updated.referenceImage.name);
            }

            if (updated.trackingState == TrackingState.None)
            {
                DisableTrackedObject(updated.referenceImage.name);
            }

            if (updated.trackingState == TrackingState.Limited)
            {
                DisableTrackedObject(updated.referenceImage.name);
            }

        }
    }

    void OnStateChanged(ARSessionStateChangedEventArgs eventArgs)
    {

        StateText.text = eventArgs.ToString();
    }
}


