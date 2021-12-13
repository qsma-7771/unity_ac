using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 basePosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // todo: obsolete
        var trackingPosition = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);

        var scale = transform.localScale;
        trackingPosition = new Vector3(
            trackingPosition.x * scale.x,
            trackingPosition.y * scale.y,
            trackingPosition.z * scale.z
        );

        // rotation
        trackingPosition = transform.rotation * trackingPosition;

        // cancel for move of hmd
        transform.position = basePosition - trackingPosition;
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger)) {
          transform.position = basePosition;
        }

        // debug
        Debug.Log(transform.GetChild(0).position);
    }
}
