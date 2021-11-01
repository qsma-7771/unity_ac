using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_compare_tile : MonoBehaviour
{

    private GameObject center;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = this.transform.position - GameObject.Find("target layer").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      if (OVRInput.Get(OVRInput.RawButton.LHandTrigger)) {
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) {
          transform.position += direction / 100;
        } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) {
          transform.position -= direction / 100;
        }
        //this.transform.position +=
      }
    }
}
