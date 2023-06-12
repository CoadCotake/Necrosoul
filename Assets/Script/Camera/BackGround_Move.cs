using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround_Move : MonoBehaviour
{
    public List<RectTransform> BackAll;
    public List<float> BackSpeed;
    static public int direction=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<BackAll.Count;i++)
        {
            BackAll[i].anchoredPosition = new Vector3(BackAll[i].anchoredPosition.x+(BackSpeed[i]*direction), BackAll[i].anchoredPosition.y);
        }
        
    }
}
