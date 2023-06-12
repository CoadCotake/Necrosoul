using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootring : MonoBehaviour
{
    public GameObject bullet;           // 총알 프리팹 - Add rigidbody && Use Gravity = false

    private float shootDuration = 0.2f;
    private float rotateSpeed = 30f;

    private void Start()
    {
        StartCoroutine(OnShoot());
    }

    private IEnumerator OnShoot()
    {
        while (true)
        {
            Debug.Log("dfa");
            GameObject go = Instantiate(bullet, this.transform.position, this.transform.rotation);  // 생성
            Vector3 direction = this.transform.TransformDirection(Vector3.forward);                 // 방향 지정
            go.GetComponent<Rigidbody>().AddForce(direction * 800);                                 // 발사

            Debug.DrawLine(this.transform.position, direction * 5, Color.blue, shootDuration);
            yield return new WaitForSeconds(shootDuration);
        }
    }

    private void Update()
    {
        // test rotation
        this.transform.localEulerAngles += Vector3.up * Time.deltaTime * rotateSpeed;
    }
}
