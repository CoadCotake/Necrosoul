using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_UI : MonoBehaviour
{
    public GameObject prfHpBar;
    public GameObject canvas;
    public Camera UI_Camera;
    public GameObject Bar;
    RectTransform hpBar;

    public float height = 1.7f;

    bool HitCheck=false;
    // Start is called before the first frame update
    private void Awake()
    {
        HitCheck=false;
        //canvas = GameObject.Find("Canvas");
      
    }

    private void Start()
    {
        Bar = Instantiate(prfHpBar, GameObject.Find("HPBarTotal").transform);
        UI_Camera = GameObject.Find("UI_Camera").GetComponent<Camera>();
        hpBar = Bar.GetComponent<RectTransform>();
        Bar.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    Vector3 _hpBarPos =
        //        Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        //    hpBar.position = _hpBarPos;
        //}
        hpBar.position = UI_Camera.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0)));
        //hpBar.position = _hpBarPos;

    }

    public void DestoryUI()
    {
        GameObject.Destroy(Bar);
    }

    public ProgressBarPro GetBar()
    {
        
        return Bar.GetComponent<ProgressBarPro>();
    }


    public void Hit()
    {
        if (!HitCheck)
        {
            Bar.SetActive(true);
            Debug.Log("체력바 생성");
        }
    }
}
