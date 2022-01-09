using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //게임 상태 상수
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    //게임 상태 변수
    public GameState gState;

    //UI 텍스트 변수
    public Text stateLabel;

    //플레이어 게임 오브젝트 변수
    GameObject player;

    //플레이어 무브 컴포넌트 변수
    PlayerMove playerM;

    //싱글턴
    public static GameManager gm;

    //옵션 메뉴 UI 오브젝트
    public GameObject optionUI;

    private void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }

    }

    void Start()
    {
        //초기 게임 상태는 준비 상태로 설정
        gState = GameState.Ready;

        //게임 시작 코루틴 함수 실행
        StartCoroutine(GameStart());

        //플레이어 오브젝트를 검색
        player = GameObject.Find("Player");

        playerM = player.GetComponent<PlayerMove>();
    }

    IEnumerator GameStart()
    {
        stateLabel.text = "Ready...";
        stateLabel.color = new Color(322, 182, 12, 255);
        yield return new WaitForSeconds(2.0f);
        stateLabel.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        stateLabel.text = "";
        gState = GameState.Run;
    }


    void Update()
    {
        //플레이어의 hp가 0이하로 떨어지는 경우
        if(playerM.hp <= 0)
        {
            stateLabel.text = "Game over...";
            stateLabel.color = new Color(255, 0, 0, 255);
            gState = GameState.GameOver;

        }
    }

    //옵션 메뉴 켜기
    public void OpenOptionWindow()
    {
        //게임 상태를 pause로 변경
        gState = GameState.Pause;

        //시간을 멈춤
        Time.timeScale = 0;

        //옵션 메뉴 창 활성화
        optionUI.SetActive(true);
    }

    //옵션 메뉴 끄기(계속하기)
    public void CloseOptionWindow()
    {
        //게임 상태를 run1로 변경
        gState = GameState.Run;

        //시간을 1배로 되돌림
        Time.timeScale = 1;

        //옵션 메뉴 창 비활성화
        optionUI.SetActive(false);
    }

    //게임 재시작하기(현재 씬 다시 로드)
    public void GameRestart()
    {
        //시간을 1배로 되돌림
        Time.timeScale = 1;

        //현재 씬 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //게임 종료
    public void GameQuit()
    {
        //어플리케이션 종료
        Application.Quit();

    }

}
