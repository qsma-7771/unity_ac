using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionParallax : MonoBehaviour
{
    Vector3 basePosition = Vector3.zero;
    public bool stationary;

    // Start is called before the first frame update
    void Start()
    {
        basePosition = transform.position;
        stationary = false;
    }

    // Update is called once per frame
    void Update()
    {
        // todo: obsolete
        //var trackingPosition = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
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
        if (stationary) {
          transform.position = basePosition - trackingPosition;
        }

        // debug
        Debug.Log(transform.GetChild(0).position);
    }
}
