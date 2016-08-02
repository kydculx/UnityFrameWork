using UnityEngine;
using System.Collections;
 
public class FWDragObject : MonoBehaviour
{
    // 마우스가 눌러졌을 때 호출되는 메소드를 코루틴(Coroutine)으로
    IEnumerator OnMouseDown ()
    {
        // 선택이 되면 색상변경
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.blue;
 
        // 오브젝트의 현재 위치를 월드좌표에서 스크린좌표로 변환
        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
 
        // 현재 마우스 좌표
        // Z값은 스크린 좌표로 설정
        Vector3 curMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
 
        // 현재 위치에서 현재 마우스 좌표값을 빼서 옵셋값 구함
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(curMousePos);
        
        // 마우스 왼쪽키를 계속 누르고 있는 상황이면..
        while (Input.GetMouseButton (0))
        {
            curMousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);

            // 새롭게 구한 위치값에 옵셋값 더함
            // 마우스 위치에 오브젝트의 센터가 달라붙는걸 방지하기 위해서
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curMousePos) + offset;
 
            transform.position = new Vector3(curPosition.x,
                                             curPosition.y,
                                             curPosition.z);
 
            yield return null;
        }
    }
 
    // 마우스 버튼이 떨어졌을 때 호출
    void OnMouseUp()
    {
        // 색상을 원래대로
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.color = Color.white;
    }
}

