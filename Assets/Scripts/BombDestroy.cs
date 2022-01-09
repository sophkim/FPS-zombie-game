using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDestroy : MonoBehaviour
{
    //생성 후 2초가 경과되면 사라짐
    float currentTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (currentTime >= 2)
        {
            Destroy(gameObject);
        }
        currentTime += Time.deltaTime;
    }
}
