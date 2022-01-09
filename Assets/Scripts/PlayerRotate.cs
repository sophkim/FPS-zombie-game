using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    //회전 속력
    public float rotSpeed = 300.0f;

    //회전 누적 변수
    float mx = 0;

    void Update()
    {
        //게임 상태 : 게임 중이 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        //사용자의 마우스 입력을 받음
        float mouse_X = Input.GetAxis("Mouse X");

        //입력받은 값을 이용해 회전 방향을 결정함
        //Vector3 dir = new Vector3(0, mouse_X, 0);
        //dir.Normalize();
        mx += mouse_X * rotSpeed * Time.deltaTime;

        //결정된 회전 방향을 물체의 회전 속성에 대입함
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
