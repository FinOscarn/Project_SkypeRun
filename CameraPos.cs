using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    //상현아...걍 이거 public으로 바꾸고 넣어라. 실행시점에서 이걸 참조해야하는데 순서가 달라짐.
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
