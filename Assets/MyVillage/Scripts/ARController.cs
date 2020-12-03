
using System.Collections.Generic;
using System.Collections.Specialized;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour {

    public Camera FirstPersonCamera;
    public GameObject Floor;
    public GameObject Character;
    public GameObject FAlert;
    public GameObject Controller;
    public GameObject DetectedPlaneVisualizer;
    private Alert a;
    private const float k_PrefabRotation = 180.0f;
    private bool m_IsQuitting = false;
    private bool first = true;
    private bool fix = false;

    public void Awake() {
        // Enable ARCore to target 60fps camera capture frame rate on supported devices.
        // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
        Application.targetFrameRate = 60;
    }

    public void Update() {
        _UpdateApplicationLifecycle();

        // If the player has not touched the screen, we are done with this update.
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) {
            return;
        }

        // Should not handle input if the player is pointing on UI.
        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
        

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)) {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0) {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else {
                if (first) {
                    // Create floor and character at the hit pose.
                    // Instantiate(prefab, hit.Pose.position, Quaternion.identity);
                    Character.SetActive(true);
                    Floor.SetActive(true);
                    
                    Controller.SetActive(true);
                }
                first = false;
                
                if(!fix) {
                    // Create an anchor to allow ARCore to track the hitpoint as understanding of
                    // the physical world evolves.
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    Character.transform.position = hit.Pose.position;
                    Floor.transform.position = hit.Pose.position;
                    

                    // Make game object a child of the anchor.
                    Character.transform.parent = anchor.transform;
                    Floor.transform.parent = anchor.transform;
                    

                    // Make alert to fix or not
                    FAlert.gameObject.SetActive(true);
                    a = FAlert.GetComponent<Alert>();
                    a.SetYesCallback(() => {
                        print("Yes callback");
                        Destroy(FAlert.gameObject);
                        fix = true;
                        DetectedPlaneVisualizer.gameObject.SetActive(false);
                    });
                    a.SetNoCallback(() => {
                        print("No callback");
                        FAlert.gameObject.SetActive(false);
                    });
                }               
            }
        }
    }

    
    private void _UpdateApplicationLifecycle() {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking) {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
        else {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting) {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted) {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError()) {
            _ShowAndroidToastMessage(
                "ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }


    /// quit the application.

    private void _DoQuit() {
        Application.Quit();
    }

    
    private void _ShowAndroidToastMessage(string message) {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null) {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject>(
                        "makeText", unityActivity, message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
