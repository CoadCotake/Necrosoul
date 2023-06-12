using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_AniManger : MonoBehaviour
{
    public Animator ContinueKey;
    public Animator Title;
    public GameObject Buttons;

    bool Frist;
    bool ButtonOn;
    // Start is called before the first frame update
    void Start()
    {
        Buttons.SetActive(false);
        Frist = true;
        ButtonOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartAnimation.OpeningEnd)
        {
            if (Input.anyKeyDown && Frist)
            {
                Frist = false;
                ContinueKey.SetBool("inputkey_C", true);
                Title.SetBool("Input", true);
            }
            if (!ButtonOn)
            {
                if (Title.GetBool("End"))
                {
                    Buttons.SetActive(true);
                    ButtonOn = true;
                }
            }
        }
    }
}
