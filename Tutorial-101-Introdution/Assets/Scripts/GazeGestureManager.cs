using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour {
    public static GazeGestureManager Instance { get; private set; }
    public GameObject FocusedObject { get; private set; }

    private GestureRecognizer _recognizer;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        _recognizer = new GestureRecognizer();
        _recognizer.TappedEvent += _recognizer_TappedEvent;
        _recognizer.StartCapturingGestures();
	}

    private void _recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        Debug.Log("Gesture: TAP");

        if (FocusedObject != null)
        {
            Debug.Log(FocusedObject);
            FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
        }
    }

    // Update is called once per frame
    void Update () {
        var oldFocusedObject = FocusedObject;

        var cameraPosition = Camera.main.transform.position;
        var direction = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if(Physics.Raycast(cameraPosition, direction, out hitInfo))
        {
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            FocusedObject = null;
        }

        if(FocusedObject != oldFocusedObject)
        {
            _recognizer.CancelGestures();
            _recognizer.StartCapturingGestures();
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (FocusedObject != null)
                FocusedObject.SendMessage("OnSelect");
        }
	}

    void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
