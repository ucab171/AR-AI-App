using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
/// Let's you place an object on an ARPlane using raycasts.
/// </summary>
[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    [SerializeField, Tooltip("The GameObject Prefab we want to instantiate when the raycast hits the plane.")]
    private GameObject prefab;

    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    /// <summary>
    /// Get references to the ARManagers on the gameobject.
    /// </summary>
    private void Awake() {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }

    /// <summary>
    /// Enable EnhancedTouch and subscribe to the finger down event.
    /// </summary>
    private void OnEnable() {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    /// <summary>
    /// Disable EnhancedTouch and unsubscribe from the finger down event.
    /// </summary>
    private void OnDisable() {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    /// <summary>
    /// Checks to see if there is user finger input using EnhancedTouch, performs a simple raycast using ARRaycast and converts the touch screen coordinates into the AR coordinates to check against trackable planes, and spawns a prefab at the hit position with the hit rotation.
    /// 
    /// Checks if the plane is the ground, and if so, rotates the gameobject to face towards the "player", or camera in this case.
    /// </summary>
    /// <param name="finger"></param>
    private void FingerDown(EnhancedTouch.Finger finger) {
        // Only execute this function if there is one finger on the screen
        if (finger.index != 0) return;

        // Cast a ray from the Touch screen coordinates using the ARRaycastManger, and checks if it hit a trackable object, in this case a plane.
        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon)) {
            foreach(ARRaycastHit hit in hits) {
                Pose pose = hit.pose;
                // Spawn the prefab in the intersection point on the plane
                GameObject obj = Instantiate(prefab, pose.position, pose.rotation);
                // Rotate the instantiated prefab towards the camera 
                if (aRPlaneManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp) {
                    Vector3 position = obj.transform.position;
                    Vector3 cameraPosition = Camera.main.transform.position;
                    Vector3 direction = cameraPosition - position;
                    Vector3 targetRotationEuler = Quaternion.LookRotation(direction).eulerAngles;
                    Vector3 scaledEuler = Vector3.Scale(targetRotationEuler, obj.transform.up.normalized); // (0, 1, 0)
                    Quaternion targetRotation = Quaternion.Euler(scaledEuler);
                    obj.transform.rotation = obj.transform.rotation * targetRotation;
                }
            }
        }
    }
}
