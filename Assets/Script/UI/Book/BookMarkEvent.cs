using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookMarkEvent : MonoBehaviour
{
    public Toggle toggle;
    public GameObject Connect_Book;
    public GameObject Image;

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
            Connect_Book.SetActive(true);
            if(PlusBook!=null)
            {
                PlusBook.SetActive(true);
            }
        }
        else
        {
            Connect_Book.SetActive(false);
            if (PlusBook != null)
            {
                PlusBook.SetActive(false);
            }
        }
    }

    public void Choice()
    {
        //Image.SetActive(true);
    }
}
