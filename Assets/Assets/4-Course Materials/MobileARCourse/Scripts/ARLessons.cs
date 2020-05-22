using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class ARLessons : MonoBehaviour
{

    [SerializeField] TMPro.TMP_Text StateText;
    [SerializeField] TMPro.TMP_Text PlanesText;
    [SerializeField] TMPro.TMP_Text DebugText;

    [SerializeField] ARPlaneManager PlaneManager;
    [SerializeField] ARPointCloudManager CloudManager;
    [SerializeField] ARRaycastManager RaycastManager;
    [SerializeField] ARCameraManager CameraManager;

    [SerializeField] Light Light;

    [SerializeField] GameObject SpawnedPrefab;

    [SerializeField] Rigidbody ballPrefab;

    string points;
    string planes;

    bool prefabActive = false;

    GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update info related to planes and cloud points
        PlanesText.text = "Planes: " + planes + " Cloud points: " + points;

        //TOUCH AND SPAWN BEHAVIOR

            //If user touches the screen
            if (Input.touchCount > 0)
            {
                //Since we have no access to the console, I put a debug text and update it accordingly to follow the code.
                DebugText.text = "Touch detected";
                Touch touch = Input.GetTouch(0);

                //If the touch is NOT on the UI
               
                    //Send a raycast from the screen point of the touch
                    var hit = new List<ARRaycastHit>();
                    bool ray = RaycastManager.Raycast(touch.position, hit, TrackableType.Planes);
                    DebugText.text ="Raycast done";

                    //If raycast hit a plane, get position info
                    if (ray)
                    {
                            //TODO Check if the touch is on event UI Elements
                         
                                DebugText.text = "Raycast hit";
                                ARRaycastHit raycastHitInfo = hit[0];
                                Vector3 position =  raycastHitInfo.pose.position;

               
                                //If the prefab is already active, move it
                                if (prefabActive == true)
                                {
                                    DebugText.text = "Prefab moved";
                                    spawnedObject.transform.position = position;
                                    //Destroy(spawnedObject);
                                    //prefabActive = false;
                                }
                                else //If the prefab is not active, instantiate it
                                {
                                    DebugText.text = "Prefab instantiated";
                                    spawnedObject = GameObject.Instantiate(SpawnedPrefab, position, Quaternion.identity);
                                    prefabActive = true;
                                }

                                    //Reset the velocity
                                    Rigidbody rbPrefab = spawnedObject.GetComponent<Rigidbody>();
                                    rbPrefab.velocity = new Vector3(0,0,0);
                                    rbPrefab.angularVelocity = new Vector3(0, 0, 0);

                                    //Loop through all child components
                                    for( int i = 0; i < spawnedObject.transform.childCount; i++)
                                     {
                                    Transform child = spawnedObject.transform.GetChild(i);
                                    Rigidbody rbChild = child.GetComponent<Rigidbody>();
                                    rbChild.velocity = new Vector3(0, 0, 0);
                                    rbChild.angularVelocity = new Vector3(0, 0, 0);
                                     }
                            

                
                     }
            }
    }


    public void ShootBall()
    {
        
            Rigidbody newBall = Instantiate(ballPrefab);
            newBall.transform.position = Camera.main.transform.position;
            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            rb.AddForce(50 * Camera.main.transform.forward, ForceMode.Impulse);
     
    }

    private void OnEnable()
    {

        ARSession.stateChanged += OnStateChanged;

        CloudManager.pointCloudsChanged += OnPointCloudChanged;
        PlaneManager.planesChanged += OnPlanesChanged;

        CameraManager.frameReceived += OnFrameReceived;


        //I use a newer version of Unity than what Coursera has, so  systemStateChanged became OnStateChanged from ARSessionStateChanged instead of ARSystemStateChanged
        void OnStateChanged(ARSessionStateChangedEventArgs eventArgs)
        {

            StateText.text = eventArgs.ToString();
        }


        void OnPointCloudChanged(ARPointCloudChangedEventArgs eventArgs)
        {
            points = "" + CloudManager.trackables.count.ToString();
        }



        void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
        {
            planes= "" + PlaneManager.trackables.count.ToString();
        }

        void OnFrameReceived(ARCameraFrameEventArgs eventArgs)
        {
            if(eventArgs.lightEstimation.averageBrightness.HasValue)
            {
                Light.intensity = eventArgs.lightEstimation.averageBrightness.Value;
            }

            if (eventArgs.lightEstimation.averageColorTemperature.HasValue)
            {
                Light.colorTemperature = eventArgs.lightEstimation.averageColorTemperature.Value;
            }

            if (eventArgs.lightEstimation.colorCorrection.HasValue)
            {
               Light.color = eventArgs.lightEstimation.colorCorrection.Value;
            }
               
        }
    }

    }