using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BackButton : MonoBehaviour
{
    public Toggle toggle;

    public GameObject PlusBook;
    // Start is called before the first frame update
    void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn)
        {
            if(BookButtonManger.bookButtonManger.GetIndex()==1)
            {
                DonDestoryManage.DDM.Reset_All();
                SceneManager.LoadScene("Main 1");
            }
        }
        else
        {
        }
    }
}
