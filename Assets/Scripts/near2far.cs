using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class near2far : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (OVRInput.Get(OVRInput.RawButton.B)) {
        SceneManager.LoadScene("shadow_far");
      }
    }
}
