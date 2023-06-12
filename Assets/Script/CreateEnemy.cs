using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{

    public GameObject door; //문
    public GameObject enemy; //적

    public int spawnCount = 0; //스폰 횟수

    // Start is called before the first frame update
    void Start()
    {
        spawnCount = 1; // 실험용 1회
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount > 0 && door.gameObject.activeSelf == true) //스폰 카운트가 있고, 문이 활성화 되어있다면
        {
            GameObject second = (GameObject)Instantiate(enemy); //적을 생성
            second.transform.position = this.transform.position;
            spawnCount--; // 소환할 때마다 스폰 카운트 감소
        }


        if (spawnCount == 0) //적이 모두 소환 되었을 때
        {
            if (!GameObject.Find("enemy(Clone)")) //적이 남아있지 않으면
            {
                gameObject.SetActive(false); // 자신(스포너)을 비활성화
            }
        }

    }
}
