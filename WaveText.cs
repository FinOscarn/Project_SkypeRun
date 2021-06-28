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
        // 우리가 변경한 값들을 메시 업데이트를 통해 반영시키는거야
        tmpText.ForceMeshUpdate();

        mesh = tmpText.mesh; // 글자 메시 정보를 가져오는 것
        vertices = mesh.vertices; // 글자들의 정점.
                                  // 각각 글자는 전부 4개의 정점으로 만들어져 있다.

        for (int i = 0; i < tmpText.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];

            // 보이지 않는 캐릭터는 처리하지 않고 continue로 보낸다.
            if (!c.isVisible)
            {
                continue;
            }
            int idx = c.vertexIndex; // 이 캐릭터의 정점 순서번호
            Vector3 offset = Wobble(Time.time + i);
            // 해당 캐릭터에는 4개의 정점이 있고 각 4개의 정점을 모두 동일한 값으로 넣는다.
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
        float x = Mathf.Sin(time * 10f) * 3f; // -3 ~ 3, 속도는 4배속
        float y = Mathf.Cos(time * 5.2f) * 3f; // -3 ~ 3, 속도는 2.2배속
        return new Vector2(x, y);
    }
}
