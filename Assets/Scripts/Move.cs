using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    GameObject rightController;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
      // Get 押してる間, GetDown 押したとき, GetUp 離した時
      // RIndexTrigger, RHandTrigger
      if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp)) {
        transform.position += transform.up * 0.03f;
      } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown)) {
        transform.position -= transform.up * 0.03f;
      } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) {
        transform.position += transform.right * 0.03f;
      } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) {
        transform.position -= transform.right * 0.03f;
      }
    }
}
