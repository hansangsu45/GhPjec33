using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DOTTest : MonoBehaviour
{
    public GameObject[] testObjs;
    public Text t;

    public Ease[] _ease;
    public KeyCode[] keycodes;

    private void Update()
    {
        GetKey();
    }

    void GetKey()
    {
        if (Input.GetKeyDown(keycodes[0]))
        {
            testObjs[0].transform.DOMove(new Vector3(5, 5, 5), 5f, false);
        }
        else if (Input.GetKeyDown(keycodes[1]))
        {
            testObjs[0].transform.DORotate(new Vector3(0, 45, 0), 3f, RotateMode.Fast);
        }
        else if (Input.GetKeyDown(keycodes[2]))
        {
            //testObjs[1].transform.DOScale(new Vector3(3, 3, 3), 1);
            testObjs[1].transform.DOMove(new Vector3(2.6f, 2.1f, 2.1f), 5, true);  //UI에도 적용이 됨
        }
        else if (Input.GetKeyDown(keycodes[3]))
        {
            testObjs[2].transform.DOShakePosition(1f, 20, 3, 90, false);
            t.DOText("타이핑 효과를 줘보자", 1.5f);
        }
        else if (Input.GetKeyDown(keycodes[4]))
        {
            testObjs[2].GetComponent<MeshRenderer>().material.DOColor(Color.red, 1);
            testObjs[3].GetComponent<MeshRenderer>().material.DOFade(0.1f, 3);
        }
        else if (Input.GetKeyDown(keycodes[5]))
        {
            //testObjs[4].transform.DOMoveY(3f, 1.2f).SetEase(_ease[0]);
            testObjs[4].transform.DOMoveY(3f, 1.2f).SetLoops(3, LoopType.Yoyo);
            //3번 반복(-1이면 무한 루프)
        }
        else if (Input.GetKeyDown(keycodes[6]))
        {
            testObjs[5].GetComponent<Camera>().DOAspect(10f, 3f);

        }
        else if (Input.GetKeyDown(keycodes[7]))
        {
            testObjs[6].GetComponent<Rigidbody>().DOJump(new Vector3(3, 4, 2), 10f, 1, 1.5f);

        }
        else if (Input.GetKeyDown(keycodes[8]))
        {
            testObjs[7].GetComponent<Light>().DOBlendableColor(Color.red, 3f);

        }
    }
}





//참고 링크 https://m.blog.naver.com/PostView.nhn?blogId=hana100494&logNo=221320177107&proxyReferer=https:%2F%2Fwww.google.com%2F
