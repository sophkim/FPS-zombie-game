using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    //회전 속력
    public float rotSpeed = 300.0f;

public float rotLimit = 60.0f;

    //회전 누적
    float mx = 0;
    float my = 0;


    void Start()
    {
        
    }

    void Update()
    {
        //게임 상태 : 게임 중이 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        //사용자의 마우스 입력을 받음
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;
        my = Mathf.Clamp(my, -rotLimit, rotLimit);

        //입력받은 값을 이용해 회전 방향을 결정함
        //Vector3 dir = new Vector3(-mouse_Y, mouse_X, 0);
        //dir.Normalize();

        //결정된 회전 방향을 물체의 회전 속성에 대입함
        //transform.eulerAngles += dir * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(-my, mx, 0);

        //회전 값 중에서 x축 값을 -90도 ~ +90도 사이로 제한
        //Vector2 rot = transform.eulerAngles;
        //rot.x = Mathf.Clamp(rot.x, -90.0f, 90.0f);
        //transform.eulerAngles = rot;
    }
}
