using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSelect : MonoBehaviour
{
    GameObject SelectWindow;
    Animator SaveAni;
    // Start is called before the first frame update
    void Start()
    {
        SelectWindow = this.gameObject;
        SelectWindow.SetActive(false);
        SaveAni = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectOn()
    {
        SelectWindow.SetActive(true);
    }
    public void SelectOff()
    {
        SaveAni.SetBool("On", true);
        
    }

    public void AniOn()
    {
        SaveAni.SetBool("On", false);
    }
    public void AniOff()
    {
        SelectWindow.SetActive(false);

    }
}
