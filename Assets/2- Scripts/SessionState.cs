using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class SessionState : MonoBehaviour
{
    // This script gets the session state and displays it in the StateText object

    [SerializeField] TMPro.TMP_Text StateText;


    private void OnEnable()
    {

        ARSession.stateChanged += OnStateChanged;
    }

    void OnStateChanged(ARSessionStateChangedEventArgs eventArgs)
    {

        StateText.text = eventArgs.ToString();
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
