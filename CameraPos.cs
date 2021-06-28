using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    //������...�� �̰� public���� �ٲٰ� �־��. ����������� �̰� �����ؾ��ϴµ� ������ �޶���.
    public Transform player;
    private Vector3 cPos;
    private Quaternion cRotate;
    private Camera m_camera;

    private float offSetX, offSetY, offSetZ;
    private void Awake()
    {

    }
    void Start()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        offSetX = 0f;
        offSetY = 3.5f;
        offSetZ = -3f;
    }

    void Update()
    {
        CameraMove();
    }
    void CameraMove()
    {
        cPos = new Vector3(player.transform.position.x + offSetX, player.transform.position.y + offSetY, player.transform.position.z + offSetZ);
        cRotate = Quaternion.Euler(20, 0, 0);
        m_camera.transform.SetPositionAndRotation(cPos,cRotate);
    }
}
