using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageContent : MonoBehaviour
{
    [Header("타이틀")]
    public GameObject Lock;
    public Image StageImage;
    public Text StageName;

    Stage stage_p;

    [Header("내용 출력")]
    public Image StageImage_C;
    public Text StageName_C;
    public Text StageClear_C;
    public Text StageDe_C;
    public Text StageExit_C;
    public Button StageMove;

    public Toggle my_toggle;

    public AudioManage_BGM m_b_audio;

    [Header("직접 넣어야 하는 것")]
    public GameObject Select;

    // Start is called before the first frame update
    void Start()
    {
        m_b_audio = AudioManage_BGM.instance;
        my_toggle = this.gameObject.GetComponent<Toggle>();
        Lock.SetActive(false);
        Select.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(my_toggle.isOn)
        {
            ButtonOn();
            Select.SetActive(false);
        }
        else
        {
            Select.SetActive(true);
        }
    }

    public void ChangeElement(Stage stage)
    {
        this.stage_p = stage;
        StageImage.sprite = stage_p.Stage_Image;
        StageName.text = stage_p.Stage_Name;
    }

    public void ButtonOn()
    {
        StageImage_C.sprite = stage_p.Stage_Image;
        StageName_C.text = stage_p.Stage_Name;
        StageDe_C.text = stage_p.Stage_Descrition;
        StageMove.onClick.RemoveAllListeners();
        StageMove.onClick.AddListener(StageButtonON);
    }

    public void StageButtonON()
    {
        m_b_audio.Stage1();
        LoadingSceneManager.l_scenemanager.LoadStage("stage1");
        //LoadingSceneManager.l_scenemanager.LoadStage(StageName_C.text);
        // SceneManager.LoadScene(StageName_C.text);
    }
}
