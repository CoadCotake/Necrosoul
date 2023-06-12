using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMyInven : MonoBehaviour
{
    public Toggle My;
    // Start is called before the first frame update

    private void Update()
    {
        PageOn();
    }

    public void PageOn()
    {
        if (!My.isOn)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
