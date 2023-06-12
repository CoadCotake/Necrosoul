using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Died : MonoBehaviour
{
    public GameObject DiedWindow;
    // Start is called before the first frame update
    public void DiedEvent()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Died", false);
        DiedWindow.SetActive(true);
        
    }   
}
