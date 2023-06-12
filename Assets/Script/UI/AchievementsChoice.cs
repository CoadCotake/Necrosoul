using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsChoice : MonoBehaviour
{
    [SerializeField] List<GameObject> Ach_penal_Data;
    [Header("텍스트 들어있는 viewport 선택")]
    public GameObject Ach_penal;
    public GameObject Window;
    public ScrollRect scroll;
    // Start is called before the first frame update

    void Start()
    {
        for(int i=0;i< Ach_penal.transform.childCount;i++)          //텍스트 창 리스트화
        {
            Ach_penal_Data.Add(Ach_penal.transform.GetChild(i).gameObject);
        }
        All_OFF();
    }


    public void All_OFF()
    {
        for (int i = 0; i < Ach_penal_Data.Count; i++)
        {
            Ach_penal_Data[i].SetActive(false);
        }
    }

    public void Poweropen()
    {
        All_OFF();
        scroll.content = Ach_penal_Data[0].GetComponent<RectTransform>();
        Ach_penal_Data[0].SetActive(true);
    }
    public void Timeopen()
    {
        All_OFF();
        scroll.content = Ach_penal_Data[1].GetComponent<RectTransform>();
        Ach_penal_Data[1].SetActive(true);
    }

    public void Completeopen()
    {
        All_OFF();
        scroll.content = Ach_penal_Data[2].GetComponent<RectTransform>();
        Ach_penal_Data[2].SetActive(true);
    }

    public void CloseWindow()
    {
        Window.SetActive(false);
    }
}
