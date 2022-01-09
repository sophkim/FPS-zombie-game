using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    //아이디 인풋 필드
    public InputField id;

    //패스워드 인풋 필드
    public InputField password;

    //상태 메세지 텍스트
    public Text message;

    void Start()
    {
        //상태 메세지를 공백으로 놓음
        message.text = "";
    }

    //새로 생성 버튼 함수
    public void SaveUserData()
    {
        //공백 입력 검사
        if(CheckInputField(id.text, password.text))
        {
            message.text = "아이디 또는 패스워드를 입력해주세요!";
            return;
        }

        //기존에 저장된 아이디가 아니라면
        if (!PlayerPrefs.HasKey(id.text))
        {
            //사용자의 아이디와 패스워드 입력을 받아 저장
            PlayerPrefs.SetString(id.text, password.text);

            //저장되었음을 메세지로 출력
            message.text = "아이디와 패스워드가 저장되었습니다.";
        }

        else
        {
            //아이디가 존재한다는 메세지 출력
            message.text = "동일한 아이디가 존재합니다.";
        }
    }

    //로그인 버튼 함수
    public void CheckUserData()
    {
        //공백 입력 검사
        if (CheckInputField(id.text, password.text))
        {
            message.text = "아이디 또는 패스워드를 입력해주세요!";
            return;
        }

        //사용자의 아이디를 키로 검색하여 저장된 패스워드를 가져옴
        string pass = PlayerPrefs.GetString(id.text);

        //가져온 패스워드와 사용자가 입력한 패스워드를 비교하여, 동일하다면
        if(pass == password.text)
        {
            SceneManager.LoadScene(1);
        }
        //그렇지 않다면 패스워드가 잘못되었다는 메세지 출력
        else
        {
            message.text = "아이디 또는 패스워드 입력이 잘못되었습니다.";
        }
        
    }

    //공백 체크 함수
    bool CheckInputField(string id, string pwd)
    {
        if(id == "" || pwd == "")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
