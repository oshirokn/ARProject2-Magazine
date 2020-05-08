using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;


public class ImageRecognitionExample : MonoBehaviour
{


    [SerializeField] ARTrackedImageManager _arTrackerImageManager;

    [SerializeField] TMPro.TMP_Text _debugText;


    public void OnEnable()
    {
        _arTrackerImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arTrackerImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            _debugText.text = "TrackedImage: " + trackedImage.name;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
