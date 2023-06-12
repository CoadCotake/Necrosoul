using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cm : MonoBehaviour
{
    Vector3 originPos;

    void Start()
    {
        originPos = transform.localPosition;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            originPos = transform.localPosition;
            StartCoroutine(Shake(0.3f,0.5f));
            ShopButton.ShopLock = false;
        }
    }
    public IEnumerator Shake(float _amount, float _duration)        //Èçµé¸² , ½Ã°£
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;

    }

    public void Cam_Shake()
    {
        originPos = transform.localPosition;
        StartCoroutine(Shake(0.3f, 0.5f));
        ShopButton.ShopLock = false;
    }
}
