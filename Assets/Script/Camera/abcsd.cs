using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abcsd : MonoBehaviour
{
    public float[] speed;
    public Transform[] backgrounds;

    public float leftPosX = 0f;
    public float rightPosX = 0f;
    public float UpPosY = 0f;
    public float DownPosY = 0f;
    public float xScreenHalfSize;
    public float yScreenHalfSize;

    void Start()
    {
        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        leftPosX = -(xScreenHalfSize * 2);
        rightPosX = xScreenHalfSize * 2;
        UpPosY = -(yScreenHalfSize * 2);
        DownPosY = yScreenHalfSize * 2;

    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed[i], -speed[i], 0) * Time.deltaTime;

            if (backgrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
            else if(backgrounds[i].position.x > rightPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + leftPosX, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }

            if (backgrounds[i].position.y < UpPosY)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x, nextPos.y + DownPosY, nextPos.z);
                backgrounds[i].position = nextPos;
            }
            else if (backgrounds[i].position.y > DownPosY)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x, nextPos.y + UpPosY, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }
}
