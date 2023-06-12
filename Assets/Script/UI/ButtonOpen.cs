using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpen : MonoBehaviour
{
    public GameObject AchPanel;
    public GameObject AchNewText;
    public void Open()
    {
        Debug.Log("업적 버튼 열기 버튼 눌렀습니다.");
        AchPanel.SetActive(true);
        AchNewText.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
