using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting_Window : MonoBehaviour
{
    
    [SerializeField]
    bool Check;
    bool SettingCheck;
    bool SettingCheck2;

    public GameObject SettingWindow;
    public GameObject SettingScene;
    public GameObject SettingGameplay;
    public GameObject SettingAudio;
    public GameObject Settingvideo;



    // Start is called before the first frame update
    void Start()
    {
        SettingWindow.SetActive(false);
        SettingScene.SetActive(false);
        SettingGameplay.SetActive(false);
        SettingAudio.SetActive(false);
        Settingvideo.SetActive(false);
        Check = false;
        SettingCheck = false;
        SettingCheck2= false;
        Time.timeScale = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC누림");
            Debug.Log("인벤 상태: "+ OpenDownInventory.check);
            if (!OpenDownInventory.check) 
            {
                if (!SettingCheck2)
                {
                    if (!SettingCheck)
                    {
                        if (Check)
                        {
                            Time.timeScale = 1;
                            SettingWindow.SetActive(false);
                            Check = false;
                        }
                        else
                        {
                            Time.timeScale = 0;
                            SettingWindow.SetActive(true);
                            Check = true;
                        }
                    }
                    else
                    {
                        SettingScene.SetActive(false);
                        SettingCheck = false;
                    }
                }
                else
                {
                    SettingGameplay.SetActive(false);
                    SettingAudio.SetActive(false);
                    Settingvideo.SetActive(false);
                    SettingCheck2 = false;
                }
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        SettingWindow.SetActive(false);
        Check = false;
    }

    public void Restart()
    {

        Player_status.p_status.Money = 0;
        ShopButton.ShopLock = true;
        Inventory.Use_InvenData.Clear();
        Inventory.Item_InvenData.Clear();
        SceneManager.LoadScene("stage1");
    }

    public void Setting()
    {
        SettingScene.SetActive(true);
        SettingCheck = true;
    }

    public void Main()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Setting_Game()
    {

    }
    public void Setting_Audio()
    {

    }

    public void Setting_video()
    {

    }

}
