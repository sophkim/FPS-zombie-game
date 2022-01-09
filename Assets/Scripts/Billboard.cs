using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    void Update()
    {
        //메인 카메라의 전방 방향과 나의 전방 방향을 일치
        transform.forward = Camera.main.transform.forward;
    }
}
