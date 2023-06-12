using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingContent : MonoBehaviour
{
    [Header("타이틀")]
    public GameObject Lock;
    public Image StageImage;
    public Text StageName;

    Stage stage_p;

    [Header("내용 출력")]
    public Image StageImage_C;
    public Text StageName_C;
    public Text StageDe_C;
    public Button StageMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene(StageName_C.text);
    }
}
