using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookMange : MonoBehaviour
{
    public List<GameObject> BookMark = new List<GameObject>();
    public GameObject BookMarkPlace;
    public List<GameObject> Book = new List<GameObject>();
    public GameObject BookPlace;

    public GameObject Text;
    

    private void Awake()
    {
        FirstSetting();

    }

    // Start is called before the first frame update
    void Start()
    {
        SceneAppear_Manager();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SceneAppear_Manager()
    {
        Debug.Log("씬이 넘어가고 어떤것을 보여줄지 고민한다.");
        this.gameObject.SetActive(false);
        Text.SetActive(false);
        if (SceneManager.GetActiveScene().name == "StageSelect")        //선택창이라면
        {
            this.gameObject.SetActive(true);
            Text.SetActive(true);
            for (int i = 0; i < BookMark.Count; i++)
            {
                BookMark[i].SetActive(true);
            }

            BookMark[1].SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {

        }
        else        //게임 플레이 창이라면
        {
            for (int i = 0; i < BookMark.Count; i++)
            {
                BookMark[i].SetActive(true);
            }
            BookMark[0].SetActive(false);
        }
        Debug.Log("씬전환아아아아ㅏ아ㅏㅇ아아아아ㅏ아아앙ㅇ아아아앙아아아앙");
    }

    public void FirstSetting()
    {
        for (int i = 0; i < BookPlace.transform.childCount; i++)
        {
            Book.Add(BookPlace.transform.GetChild(i).gameObject);
        }
        for (int i=0; i<BookMarkPlace.transform.childCount;i++)
        {
            BookMarkPlace.transform.GetChild(i).GetComponent<BookMarkEvent>().Connect_Book = Book[i];
            BookMark.Add(BookMarkPlace.transform.GetChild(i).gameObject);

        }

    }

    void OnEnable()
    {
        // 델리게이트 체인 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneAppear_Manager();
    }

    void OnDisable()
    {
        // 델리게이트 체인 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
