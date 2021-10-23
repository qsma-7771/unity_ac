using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataLogger : MonoBehaviour
{
    private StreamWriter sw;
    private FileInfo fi;
    private DateTime date;

    // Start is called before the first frame update
    void Start()
    {
        //var combinedPath = Path.Combine(Application.persistentDataPath, "hoge");
        using (var streamWriter = new StreamWriter($"/storage/emulated/0/Download/hoge"))
        {
            streamWriter.WriteLine("outputting test");
        }

        var sampleData = "SampleText";
        CSVSave(sampleData, "sampleFile");
    }

    // Update is called once per frame
    void Update()
    {
      if (OVRInput.Get(OVRInput.RawButton.A)) {
        Debug.Log("position = " + (transform.position.x) + "," +(transform.position.y) + "," + (transform.position.z)+","+ transform.rotation.x +","+ transform.rotation.y +","+ transform.rotation.z +","+ transform.rotation.w);

        StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/position.csv", append:true, System.Text.Encoding.UTF8);
        sw.WriteLine((transform.position.x) + "," +(transform.position.y) + "," + (transform.position.z)+","+ transform.rotation.x +","+ transform.rotation.y +","+ transform.rotation.z +","+ transform.rotation.w);
        sw.Flush();
        sw.Close();
      }
    }
    private void CSVSave(string data, string fileName)
    {
        //sw = new StreamWriter("D:/codic/labo/unity/test.csv", false, Encoding.GetEncoding("Shift_JIS"));
        StreamWriter sw;
        FileInfo fi;
        DateTime now = DateTime.Now;

        fileName = fileName + now.Year.ToString() + "_" + now.Month.ToString() + "_" + now.Day.ToString() + "__" + now.Hour.ToString() + "_" + now.Minute.ToString() + "_" + now.Second.ToString();
        fi = new FileInfo(Application.dataPath + "/" + fileName + ".csv");
        sw = fi.AppendText();
        sw.WriteLine(data);
        sw.Flush();
        sw.Close();
        Debug.Log("Save Completed");
    }
}
