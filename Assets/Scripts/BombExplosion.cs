using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    //폭발 효과 프리펩 변explosionRadios
    public GameObject explosion;

    //폭발 반경
    public float explosionRadius = 5.0f;

    //수류탄 데미지 변수
    public int bombPower = 3;

    private void OnCollisionEnter(Collision collision)
        {
            //충돌한다면 폭발 효과 이펙트를 생성
            GameObject go = Instantiate(explosion);
            go.transform.position = transform.position;

            //자신의 위치에서 일정한 반경만큼을 검색해서 그 범위 안의 에너미들을 찾음
            Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);

            //검출된 에너미들에게 수류탄 데미지를 입힌다.
            for(int i = 0; i < enemies.Length; i++)
            {
                EnemyFSM eFSM = enemies[i].transform.GetComponent<EnemyFSM>();
                eFSM.HitEnemy(bombPower);
            }

            //자신을 제거
            Destroy(gameObject);
        }
    
}
