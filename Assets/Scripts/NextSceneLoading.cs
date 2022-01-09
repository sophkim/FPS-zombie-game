using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextSceneLoading : MonoBehaviour
{
    //로딩 슬라이더
    public Slider loadingBar;

    //로딩 텍스트
    public Text loadingText;

    //씬 번호
    public int sceneNum = 2;

    void Start()
    {
        //비동기 로딩 씬 코루틴 함수 실행
        StartCoroutine(LoadNextScene(sceneNum));
    }

    //비동기 로딩 씬 코루틴 함수
    IEnumerator LoadNextScene(int num)
    {
        //비동기 씬 로드 시작
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        //로드되는 씬의 모습을 화면에 보이지 않도록 함
        ao.allowSceneActivation = false;

        //로딩 끝나서 전환되기 전까지 진행 결과 표시
        while(!ao.isDone)
        {
            loadingBar.value = ao.progress;
            loadingText.text = (ao.progress * 100.0f).ToString() + "%";

            //만 로딩 진행도가 90% 이상이라면, 씬을 화면에 보일 수 있게 함
            if(ao.progress >= 0.9f)
            {
                ao.allowSceneActivation = true;
            }

            //다음 프레임으로 넘김
            yield return new WaitForSeconds(1.5f);
            //yield return null;
        }
    }

}
