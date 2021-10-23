using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class far_near_cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      transform.position = new Vector3(0,1,3);
    }

    // Update is called once per frame
    void Update()
    {
      if (OVRInput.Get(OVRInput.RawButton.A)) {
        transform.position = new Vector3(0,1,3);
        //transform.localScale = Vector3.one * 0.5;
      }
      if (OVRInput.Get(OVRInput.RawButton.B)) {
        transform.position = transform.up + transform.forward * 6f;
      }
      transform.localScale = new Vector3(1,1,1) * transform.position.z / 6f;
    }
}
