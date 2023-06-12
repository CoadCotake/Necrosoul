using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Init_Splash : MonoBehaviour
{
    GameObject SplashObj;               //판넬오브젝트
    Image image;                            //판넬 이미지
    Color color;

    private bool checkbool = false;     //투명도 조절 논리형 변수


    void Awake()
    {
        SplashObj = this.gameObject;                         //스크립트 참조된 오브젝트
        image = SplashObj.GetComponent<Image>();    //판넬오브젝트에 이미지 참조
        color = image.color;
    }


    void Update()
    {
        StartCoroutine("MainSplash");                        //코루틴    //판넬 투명도 조절

        if (checkbool)                                            //만약 checkbool 이 참이면
        {
            color.a = 1;                      //판넬 파괴, 삭제
            checkbool = false;
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator MainSplash()
    {

        for (int i = 100; i >= 0; i--)                            //for문 100번 반복 0보다 작을 때 까지
        {

            color.a -= Time.deltaTime * 0.005f;               //이미지 알파 값을 타임 델타 값 * 0.01

            image.color = color;                                //판넬 이미지 컬러에 바뀐 알파값 참조

            if (image.color.a <= 0)                        //만약 판넬 이미지 알파 값이 0보다 작으면
            {
                checkbool = true;                              //checkbool 참 
                Gamemanager.GM.fade_in_complete=true;
            }
        }
        yield return null;                                        //코루틴 종료
    }

}
