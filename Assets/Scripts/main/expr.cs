using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class expr : MonoBehaviour
{

    [SerializeField] public Button button1;
    [SerializeField] public Button button2;

    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(() => SceneManager.LoadScene("expr1") );
        button2.onClick.AddListener(() => SceneManager.LoadScene("expr2") );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
