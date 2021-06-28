using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealGenerator : MonoBehaviour
{
    public Transform playerTrm;
    public Transform startPosition;
    public GameObject playerObj;
    public GameObject tileDown;

    private Vector3 lastPosition;
    

    private Vector3 blockSize;

    private Queue<Transform> m_queue = new Queue<Transform>();
    private Queue<Transform> m_outQueue = new Queue<Transform>();
    void Awake()
    {
        for (int i = 0; i < 60; i++)
        {
            GameObject bottom = Instantiate(tileDown, transform.position, Quaternion.identity, transform);
            bottom.SetActive(false);
            m_queue.Enqueue(bottom.transform);
        }

        //첫번째 블럭의 크기를 알아내고
        Transform first = m_queue.Peek();
        MeshRenderer renderer = first.GetComponent<MeshRenderer>();
        blockSize = new Vector3(0, 0, renderer.bounds.size.z);

        lastPosition = startPosition.position;

        for (int i = 0; i < 25; i++)
        {
            //먼저 25개의 블럭을 생성
            GenerateBlock();
        }
    }
    private int ThreeRandom(int a)
    {
        int b;
        b = Random.Range(-a,a);

        if((b!=-a) || (b!=0) || (b!=a))
        {
            b = Random.Range(-a, a);
        }
        return b;
    }

    private void GenerateBlock()
    {
        Transform bottom = m_queue.Dequeue();
        
        bottom.position = lastPosition + blockSize+ new Vector3(ThreeRandom(6),0,Random.Range(-5,20) + playerObj.GetComponent<PlayerMove>().speed);
        lastPosition = bottom.position;
        bottom.gameObject.SetActive(true);

        m_outQueue.Enqueue(bottom); //아웃큐에 넣어준다.
    }

    void Update()
    {
        if (m_outQueue.Count < 1) return;

        Transform last = m_outQueue.Peek();
        if (last.position.z + blockSize.z < playerTrm.position.z)
        {
            last = m_outQueue.Dequeue();      
            last.gameObject.SetActive(false);

            m_queue.Enqueue(last);

            GenerateBlock();
        }
    }
}