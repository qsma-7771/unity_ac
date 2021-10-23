using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBall : MonoBehaviour
{
    [SerializeField]
    GameObject rightController;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            //球を生成（「ヒエラルキーで右クリック→Create→3D Object→Sphere」と同じ作業です）
            GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            //生成した球の位置を右手コントローラの位置に変更
            go.transform.position = rightController.transform.position;
            //生成した球のサイズを半径0.1mに変更
            go.transform.localScale *= 0.1f;

            //物理演算用のコンポーネントを追加
            var rb = go.AddComponent<Rigidbody>();

            rb.AddForce(rightController.transform.forward*10.0f,ForceMode.Impulse);
        }
    }
}
