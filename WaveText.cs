using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    private TMP_Text tmpText;

    Mesh mesh;
    Vector3[] vertices;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }


    private void Update()
    {
        // �츮�� ������ ������ �޽� ������Ʈ�� ���� �ݿ���Ű�°ž�
        tmpText.ForceMeshUpdate();

        mesh = tmpText.mesh; // ���� �޽� ������ �������� ��
        vertices = mesh.vertices; // ���ڵ��� ����.
                                  // ���� ���ڴ� ���� 4���� �������� ������� �ִ�.

        for (int i = 0; i < tmpText.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            // ������ �ʴ� ĳ���ʹ� ó������ �ʰ� continue�� ������.
            if (!c.isVisible)
            {
                continue;
            }
            int idx = c.vertexIndex; // �� ĳ������ ���� ������ȣ
            Vector3 offset = Wobble(Time.time + i);
            // �ش� ĳ���Ϳ��� 4���� ������ �ְ� �� 4���� ������ ��� ������ ������ �ִ´�.
            for (int j = 0; j < 4; j++)
            {
                vertices[idx + j] += offset;
            }
        }

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }

    private Vector2 Wobble(float time)
    {
        float x = Mathf.Sin(time * 10f) * 3f; // -3 ~ 3, �ӵ��� 4���
        float y = Mathf.Cos(time * 5.2f) * 3f; // -3 ~ 3, �ӵ��� 2.2���
        return new Vector2(x, y);
    }
}
