using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (OVRInput.Get(OVRInput.RawButton.A)) {
        transform.position = new Vector3(0,1,3);
      }
      if (OVRInput.Get(OVRInput.RawButton.B)) {
        transform.position = new Vector3(0,1,6);
      }
      if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
        transform.position = new Vector3(0,1,6);
      }

      if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp)) {
        transform.position += new Vector3(0,0,1f)/100;
      } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown)) {
        transform.position -= new Vector3(0,0,1f)/100;
      }
      transform.localScale = new Vector3(1f,1f,1f) * (transform.position.z/6f);
    }
}
