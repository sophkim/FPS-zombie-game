using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
	//전후좌우 입력을 받아서 그 방향으로(수평이동)
	public float moveSpeed = 7.0f;
	CharacterController cc;
    
	//캐릭터에 중력 적용(수직 이동)
	public float gravity = -20.0f;

	//수직 속도 
	float yVelocity = 0;

	//점프력
	public float jumpPower = 10;

	//최대 점프 횟수
	public int maxJump = 2;

	//현재 점프 횟수
	int jumpCount = 0;

	//체력 변수
	public int hp;

	//최대 체력
	public int maxHp = 10;

	//슬라이더 UI
	public Slider hpSlider;

	//이펙트 UI 오브젝트
	public GameObject hitEffect;

	//애니메이터 컴포넌트 변수
	Animator anim;

    void Start()
    {
        //캐릭터 컨트롤러 컴포넌트를 받아옴
		cc = GetComponent<CharacterController>();

		//체력 변수 초기화
		hp = maxHp;

		//자식 오브젝트의 애니메이터 컴포넌트를 가져옴
		anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
		//슬라이더의 value를 체력 비율로 적용
		hpSlider.value = (float)hp / (float)maxHp;

		//게임 상태 : 게임 중이 아니면 업데이트 함수 종료
		if (GameManager.gm.gState != GameManager.GameState.Run)
        {
			return;
        }

        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		//이동 방향 설정
		Vector3 dir = new Vector3(h, 0, v);
		dir.Normalize();

		//이동 방향 벡터의 크기 값을 애니머이터의 이동 블랜드 트리에 전달
		anim.SetFloat("MoveDirection", dir.magnitude);

		//이동 방향(월드 좌표)을 카메라 방향(로컬 좌표) 기준으로 전환
		dir = Camera.main.transform.TransformDirection(dir);

		//플레이어가 땅에 착지하면 현재 점프 횟수를 0으로 초기화, 수직 속도 값(중력)을 0으로 초기화
		if (cc.collisionFlags == CollisionFlags.Below)
			{
				jumpCount = 0;
				yVelocity = 0;
			}
	
		//점프 키, 단 현재 점프 횟수가 최대 점프 횟수를 넘어가지 않아야 함
		if(Input.GetButtonDown("Jump") && jumpCount < maxJump)
			{
				jumpCount++;
				yVelocity = jumpPower;
			}

		//캐릭터의 수직속도(중력) 적용
		yVelocity += gravity * Time.deltaTime;
		dir.y = yVelocity;

		//이동 방향으로 플레이어 이동
		//transform.position += dir * moveSpeed * Time.deltaTime;
		cc.Move(dir * moveSpeed * Time.deltaTime);		
    }

	//플레이어 피격 함수
	public void OnDamage(int value)
	{
		hp -= value;
		if (hp < 0)
		{
			hp = 0;
		}

		else
        {
			StartCoroutine(HitEffect());
        }
	}

	IEnumerator HitEffect()
    {
		hitEffect.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		hitEffect.SetActive(false);
    }
}
