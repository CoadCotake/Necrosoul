using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject spawner; // 스포너

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf == true) //자신(문)이 활성화 되어 있으면
        {
            
            if (spawner.gameObject.activeSelf == false) // 스포너가 계속 있는지 체크
            {
                
                gameObject.SetActive(false); // 스포너가 없어지면 자신도 제거 
            }
        }
    }
}
