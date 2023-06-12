using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchSpawner : MonoBehaviour
{
    public GameObject door;
    public GameObject spawn;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") //플레이어가 센서에 충돌했을 때
        {
            if (GameObject.Find("spawn")) //씬안에 스포너가 있으면
            {
                door.SetActive(true); //문을 활성화
            }
        }
    }



}
