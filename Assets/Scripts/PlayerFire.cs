using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    //수류탄 오브젝트
    public GameObject bombFactory;

    //발사할 위치
    public Transform firePosition;

    //발사할 힘
    public float throwPower = 10.0f;

    //총알 이펙트 게임 오브젝트
    public GameObject bulletEffect;

    //파티클 시스템 변수
    ParticleSystem ps;

    //총알 공격력
    public int attackPower = 2;

    //애니메이터 컴포넌트
    Animator anim;

    //무기 아이콘 스프라이트 변수
    public GameObject weapon01;
    public GameObject weapon02;

    //크로스헤어 스프라이트 변수
    public GameObject crosshair01;
    public GameObject crosshair02;

    //마우스 오른쪽 버튼 클릭 했을 때 스프라이트 변수
    public GameObject weapon01_R;
    public GameObject weapon02_R;

    //마우스 오른쪽 버튼 클릭 줌인 모 스프라이트 변수
    public GameObject crosshair02_zoom;

    //게임 모드 상수
    enum WeaponMode
    {
        Normal,
        Sniper
    }

    WeaponMode wMode;

    //카메라 줌인 줌아웃 체크 변수
    bool isZoom = false;

    //무기 모드 텍스트
    public Text weaponText;

    //총구 이펙트 배열
    public GameObject[] eff_Flash;


    void Start()
    {
        //파티클 시스템 컴포넌트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();

        //자식 오브젝트에서 애니메이터 가져오기
        anim = GetComponentInChildren<Animator>();

        //초기 무기 모드는 일반 모드로 함
        wMode = WeaponMode.Normal;
        weaponText.text = "Normal";
    }

    void Update()
    {
        //게임 상태 : 게임 중이 아니면 업데이트 함수 종료
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        //마우스 좌 클릭시
        if (Input.GetMouseButtonDown(0))
        {
            //레이 생성
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //레이에 부딪힌 대상의 정보를 저장할 변수
            RaycastHit hitInfo = new RaycastHit();

            //레이 발사 후 부딪힌 대상이 있다면
            if(Physics.Raycast(ray, out hitInfo))
            {
                //부딛힌 대상의 레이어가 Enemy라면
                if(hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(attackPower);
                }

                //부딭힌 위치에 총알 이펙트 오브젝트를 위치시킴
                bulletEffect.transform.position = hitInfo.point;

                //총알 이펙트의 방향을 부딪힌 오브젝트의 수직 방향과 일치시킴
                bulletEffect.transform.forward = hitInfo.normal;

                //총알 이펙트를 플레이
                ps.Play();
            }

            //만약, 블랜드 트리의 MoveDirection 파라미터의 값이 0이면
            if (anim.GetFloat("MoveDirection") == 0)
            {
                //총 발사 애니메이션 플레이
                anim.SetTrigger("Attack");
            }

            //총구 이펙트 코루틴 함수 실행
            StartCoroutine(ShootEffect(0.1f));
        }

        //마우스 우 클릭시
        if (Input.GetMouseButtonDown(1))
        {
            //만약 무기 모드가 노멀 모드라면, 수류탄을 투척함
            //만약 무기 모드가 스나이퍼 모드라면, 카메라 줌인 줌아웃을 함
            switch(wMode)
            {
                case WeaponMode.Normal:
                    //수류탄 생성
                    GameObject bomb = Instantiate(bombFactory);
                    bomb.transform.position = firePosition.position;

                    //수류탄의 리지드 바디 컴포넌트를 받아옴
                    Rigidbody rb = bomb.GetComponent<Rigidbody>();

                    //시선 방향으로 발사
                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
                    break;

                case WeaponMode.Sniper:
                    //줌아웃 상태라면, 줌인 상태로 만들고 카메라의 시야각(FoV)을 15도로 변경
                    if(!isZoom)
                    {
                        isZoom = true;
                        Camera.main.fieldOfView = 15.0f;

                        //줌인 상태일 때 크로스 헤어 변경
                        crosshair02_zoom.SetActive(true);
                        crosshair02.SetActive(false);
                    }

                    //줌인 상태리면, 줌아웃 상태로 만들로 카메라의 시야각을 60도로 변경
                    else
                    {
                        isZoom = false;
                        Camera.main.fieldOfView = 60.0f;

                        //줌모드를 끄고 크로스 헤어를 스나이프 모드로 돌려놓음
                        crosshair02_zoom.SetActive(false);
                        crosshair02.SetActive(true);
                    }

                    break;
            }       

        }

        //숫자 키 입력이 1번이면 노멀 모드, 2번이면 스나이퍼 모드로 전환
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;
            weaponText.text = "Normal Mode";

            //줌아웃 상태로 전환
            Camera.main.fieldOfView = 60.0f;
            isZoom = false;

            //1번 스프라이트 활성화, 2번 스프라이트 비활성화
            weapon01.SetActive(true);
            weapon02.SetActive(false);
            crosshair01.SetActive(true);
            crosshair02.SetActive(false);

            //Weapon01_R은 활성화 되고 Weapon02_R은 비활성화 된다
            weapon01_R.SetActive(true);
            weapon02_R.SetActive(false);

            //스나이퍼 모드에서 일반 모드 키 눌렀을 때
            crosshair02_zoom.SetActive(false);

        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;
            weaponText.text = "Sniper Mode";

            //1번 스프라이트 비활성화, 2번 스프라이트 비활성화
            weapon01.SetActive(false);
            weapon02.SetActive(true);
            crosshair01.SetActive(false);
            crosshair02.SetActive(true);

            //Weapon01_R은 비활성화 되고 Weapon02_R은 활성화 된다
            weapon01_R.SetActive(false);
            weapon02_R.SetActive(true);
        }
    }

    //총구 이펙트 코루틴 함수
    IEnumerator ShootEffect(float duration)
    {
        //다섯 개의 이펙트 오브젝트 중 랜덤으로 1개 고름
        int num = Random.Range(0, eff_Flash.Length - 1);

        //선택된 오브젝트 활성화 시킴
        eff_Flash[num].SetActive(true);

        //일정 시간(duration)동안 기다림
        yield return new WaitForSeconds(duration);

        //활성화된 오브젝트 를 다시 비활성화 시킴
        eff_Flash[num].SetActive(false);

    }
}
