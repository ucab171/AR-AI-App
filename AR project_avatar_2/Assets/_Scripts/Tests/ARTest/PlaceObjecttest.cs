

using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#if UNITY_EDITOR
public interface IARPlaneManagerWrapper
{
    ARPlane GetPlane(TrackableId trackableId);
}

public interface IARRaycastManagerWrapper
{
    bool Raycast(Vector2 screenPosition, List<ARRaycastHit> hitResults, TrackableType trackableType);

}
public class ARPlaneManagerWrapper : IARPlaneManagerWrapper
{
    private readonly ARPlaneManager _planeManager;

    public ARPlaneManagerWrapper(ARPlaneManager planeManager)
    {
        _planeManager = planeManager;
    }

    public ARPlane GetPlane(TrackableId trackableId)
    {
        return _planeManager.GetPlane(trackableId);
    }
}

public class ARRaycastManagerWrapper : IARRaycastManagerWrapper
{
    private readonly ARRaycastManager _raycastManager;

    public ARRaycastManagerWrapper(ARRaycastManager raycastManager)
    {
        _raycastManager = raycastManager;
    }

    public bool Raycast(Vector2 screenPosition, List<ARRaycastHit> hitResults, TrackableType trackableType)
{
    return _raycastManager.Raycast(screenPosition, hitResults, trackableType);
}
}

public class PlaceObjectTests
{
    private PlaceObject placeObject;
    private IARRaycastManagerWrapper mockRaycastManager; // Using the interface type here
    private IARPlaneManagerWrapper mockPlaneManager;


    [SetUp]
    public void SetUp()
    {
        var go = new GameObject();
        placeObject = go.AddComponent<PlaceObject>();

        mockRaycastManager = Substitute.For<IARRaycastManagerWrapper>();
        mockPlaneManager = Substitute.For<IARPlaneManagerWrapper>();


        typeof(PlaceObject).GetField("aRRaycastManagerWrapper", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(placeObject, mockRaycastManager);
        typeof(PlaceObject).GetField("aRPlaneManager", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(placeObject, mockPlaneManager);
    }

    [Test]
    public void FingerDown_SpawnsObject_WhenRaycastHit()
    {
        Pose mockPose = new Pose(Vector3.zero, Quaternion.identity);
        TrackableId mockTrackableId = new TrackableId(1, 2);
        ARTrackable mockTrackable = Substitute.For<ARTrackable>();
        TrackableType mockTrackableType = TrackableType.PlaneWithinPolygon;
        float mockDistance = 1.0f;
        XRRaycastHit xrRaycastHit = new XRRaycastHit(mockTrackableId, mockPose, mockDistance, mockTrackableType);
        var mockHit = new ARRaycastHit(xrRaycastHit, mockDistance, mockTrackable.transform);

        var mockList = new List<ARRaycastHit> { mockHit };
        Vector2 dummyScreenPosition = new Vector2(0, 0);

        mockRaycastManager.Raycast(dummyScreenPosition, null, TrackableType.PlaneWithinPolygon).ReturnsForAnyArgs(x => 
        {
            x[1] = mockList;
            return true;
        });
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.Destroy(placeObject.gameObject);
    }
}
#endif