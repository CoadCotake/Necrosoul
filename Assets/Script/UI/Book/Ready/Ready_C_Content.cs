using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ready_C_Content : MonoBehaviour
{
    [SerializeField] bool Click;

    [Header("컨텐츠 요소")]
    public Text Title;
    public GameObject UnLock;
    public GameObject Select;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void In_Click()
    {
        Select.SetActive(false);
        Debug.Log("캐릭터 선택 ");
    }

    public void Un_Click()
    {
        Select.SetActive(true);
        Debug.Log("캐릭터 선택하지 않음 ");
    }
}
