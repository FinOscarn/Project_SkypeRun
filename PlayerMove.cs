using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    enum MyDir
    {
        Left,
        Right,
        Up,
        Down,
        Run
    }

    public float speed = 7f;
    private int jump = 2;
    private bool isGround = true;

    private GameObject character;
    private Rigidbody playerRg;

    private Vector3 endPos, startPos, cPos;
    private MyDir currentXDir, currentYDir;
    private float currentPX, currentPY, characterPZ;

    public float currentPZ { get; private set; }




    private float timeSet = 0;

    private void Awake()
    {
        // �ڷ��� ���� ��������Ʈ
        character = GameObject.FindWithTag("Player");
        characterPZ = transform.position.z;
        currentPZ = character.transform.position.z;
        playerRg = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    void Update()
    {
        timeSet += Time.deltaTime;
        // �÷��̾� ��ġ�� �޾ƿ���
        currentPX = character.transform.position.x;
        currentPY = character.transform.position.y;
        currentPZ = character.transform.position.z;
        //Debug.Log("z�� / ���ǵ� "+currentPZ / speed)

        MousePosition();
    }

    private void FixedUpdate()
    {
        float acceleration = 0.01f;

        playerRg.velocity = new Vector3(playerRg.velocity.x, playerRg.velocity.y, speed+=acceleration);
    }

    void PlayerXMovement()
    {
        float acceleration = 0.01f;
        switch (currentXDir)
        {
            case MyDir.Left:
                currentPX -= 1;
                transform.position = character.transform.position = new Vector3(currentPX, currentPY, currentPZ);
                //StartCoroutine("RightMove");
                break;

            case MyDir.Right:
                currentPX += 1;
                transform.position = character.transform.position = new Vector3(currentPX, currentPY, currentPZ);
                //StartCoroutine("LeftMove");
                break;

            case MyDir.Run:
                playerRg.velocity = new Vector3(playerRg.velocity.x, playerRg.velocity.y, speed += acceleration);
                break;
        }
    }
    void PlayerYMovement()
    {
        float acceleration = 0.01f;
        switch (currentYDir)
        {
            case MyDir.Up:
                if(isGround)
                {
                    playerRg.velocity = Vector3.zero;
                    if (jump>0)
                    {
                        playerRg.AddForce(new Vector3(0, cPos.y, 0), ForceMode.Impulse);
                        jump --;
                    }
                    else if(jump<=0)
                    {
                        break;
                    }
                    break;
                }
                break;

            case MyDir.Run:
                playerRg.velocity = new Vector3(playerRg.velocity.x, playerRg.velocity.y, speed += acceleration);
                break;
        }
    }
    MyDir CheckXDir()
    {
        // �÷��̾� y�� ����
        // �÷��̾� x�� ����
        if ((endPos.x - startPos.x) > 0)
        {
            //����
            return MyDir.Right;
        }
        else if ((endPos.x - startPos.x) < 0)
        {
            //����
            return MyDir.Left;
        }
        else
            return MyDir.Run;
    }

    IEnumerator LeftMove()
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = character.transform.position = new Vector3(currentPX, currentPY, currentPZ);
    }
    IEnumerator RightMove()
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = character.transform.position = new Vector3(currentPX, currentPY, currentPZ);
    }

    MyDir CheckYDir()
    {
        if ((endPos.y - startPos.y) > 0)
        {
            //����
            return MyDir.Up;
        }
        else if ((endPos.y - startPos.y) < 0)
        {
            //�����̵�
            return MyDir.Down;
        }
        else
            return MyDir.Run;
    }
    void MousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            cPos = endPos - startPos;

            if (Mathf.Abs(cPos.x) > Mathf.Abs(cPos.y)) // ���������� ������ x�࿡ �� ����� ��
            {
                currentXDir = CheckXDir();
                PlayerXMovement();
            }
            if (Mathf.Abs(cPos.y) > Mathf.Abs(cPos.x)) // ���������� ������ y�࿡ �� ����� �� 
            {
                currentYDir = CheckYDir();
                PlayerYMovement();
            }
        }
    }
    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Ground") {
            isGround = true;
            jump = 2;
        }
    }
}