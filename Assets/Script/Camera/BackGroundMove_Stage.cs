using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove_Stage : MonoBehaviour
{
    static public BackGroundMove_Stage background_M;
    public float[] speed;
    public GameObject BackgroundPlace;
    public List<Transform> backgrounds=new List<Transform>();

    public float leftPosX = 0f;
    public float rightPosX = 0f;
    public float UpPosY = 0f;
    public float DownPosY = 0f;
    public float xScreenHalfSize;
    public float yScreenHalfSize;
    float l;
    void Start()
    {
        background_M = this;
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        leftPosX = -(xScreenHalfSize * 2);
        rightPosX = xScreenHalfSize * 2;
        UpPosY = -(yScreenHalfSize * 2);
        DownPosY = yScreenHalfSize * 2;

        for(int i=0; i<BackgroundPlace.transform.childCount;i++)
        {
            backgrounds.Add(BackgroundPlace.transform.GetChild(i));
        }

    }

    void Update()
    {
        
        if (Gamemanager.GM.game_ev.l>0.3f)
        {
            Movebackground(1);
        }
        else if (Gamemanager.GM.game_ev.l < -0.3f)
        {
            Movebackground(-1);
        }
    }

    public void Movebackground(int dir)
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].position += new Vector3(-speed[i]*dir, 0, 0) * Time.deltaTime;

            if (backgrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
            else if (backgrounds[i].position.x > rightPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + leftPosX, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }

            //if (backgrounds[i].position.y < UpPosY)   위로 움직임
            //{
            //    Vector3 nextPos = backgrounds[i].position;
            //    nextPos = new Vector3(nextPos.x, nextPos.y + DownPosY, nextPos.z);
            //    backgrounds[i].position = nextPos;
            //}
            //else if (backgrounds[i].position.y > DownPosY)
            //{
            //    Vector3 nextPos = backgrounds[i].position;
            //    nextPos = new Vector3(nextPos.x, nextPos.y + UpPosY, nextPos.z);
            //    backgrounds[i].position = nextPos;
            //}
        }
    }
}
