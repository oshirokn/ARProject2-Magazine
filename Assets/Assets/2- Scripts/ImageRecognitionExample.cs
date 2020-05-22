using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;


public class ImageRecognitionExample : MonoBehaviour
{


    [SerializeField] ARTrackedImageManager _arTrackerImageManager;
    [SerializeField] TMPro.TMP_Text StateText;

    [SerializeField] TMPro.TMP_Text _debugText;
    [SerializeField] TMPro.TMP_Text _spawnedPrefabsText;

    [SerializeField] GameObject[] prefabs;
    [SerializeField] List<GameObject> spawnedPrefabs;

    public void OnEnable()
    {
        _arTrackerImageManager.trackedImagesChanged += OnImageChanged;

        ARSession.stateChanged += OnStateChanged;

    }
    public void OnDisable()
    {
        _arTrackerImageManager.trackedImagesChanged -= OnImageChanged;
    }

    void OnStateChanged(ARSessionStateChangedEventArgs eventArgs)
    {
        StateText.text = eventArgs.ToString();
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {

        //Go through all the tracked images that have been added and instantiate a prefab with the same name from the prefab array. 
        //Also add the prefab to the list of spawned prefabs.

        foreach (var trackedImage in args.added)
        {
            _debugText.text = "TrackedImage: " + trackedImage.referenceImage.name;
            _debugText.text = "Prefab to spawn: " + prefabs[int.Parse(trackedImage.referenceImage.name)];

            GameObject spawnedObject = GameObject.Instantiate(prefabs[1], trackedImage.transform.position, Quaternion.identity);

            //GameObject spawnedObject = GameObject.Instantiate(prefabs[int.Parse(trackedImage.referenceImage.name)], trackedImage.transform.position, Quaternion.identity);
            spawnedPrefabs.Add(spawnedObject); 

            _spawnedPrefabsText.text = spawnedObject.name;
        }

        //Go through all the tracked images that have been removed and find the corresponding object in the list of spawned prefabs.
        //Remove the prefab from the list and destroy the gamer object
        
        foreach (var trackedImage in args.removed)
        {
            _debugText.text = "Removed TrackedImage: " + trackedImage.referenceImage.name;
            _spawnedPrefabsText.text = spawnedPrefabs.ToString();

            foreach (GameObject prefab in spawnedPrefabs)
            {
                if(prefab.name == trackedImage.referenceImage.name)
                {
                    spawnedPrefabs.Remove(prefab);
                    Object.Destroy(prefab);
                }
            }
            
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
