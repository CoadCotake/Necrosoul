using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background_scrol : MonoBehaviour
{
    public GameObject BackGround;
    public GameObject Image;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(BackGround_Move.direction>0) // 뒤쪽으로 이동
        {
            if (BackGround.transform.position.x>0)
            {
                Image.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1920, 0);
            }
            
        }
       
    }
}
