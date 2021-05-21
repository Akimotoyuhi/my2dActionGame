using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaitenSample : MonoBehaviour
{
    ///// <summary> 中心点 </summary>
    //Vector3 point = new Vector3(20, 5, 0);
    ///// <summary> 回転軸 </summary>
    //Vector3 axis = Vector3.up;
    ///// <summary> 円運動周期 </summary>
    //float period = 0.1f;

    /// <summary> 移動速度 </summary>
    [SerializeField] float speed = 10f;
    /// <summary> 円の大きさ </summary>
    float radius = 3f;

    float x = 0;
    float y = 0;
    Vector2 defPos;
    //[SerializeField] float delayTime = 0;

    private void Start()
    {
        defPos = transform.position;
        //StartCoroutine("Delay");
    }

    void Update()
    {
        //transform.RotateAround(point, axis, 360 / period * Time.deltaTime);

        x = radius * Mathf.Sin(Time.time * speed);
        y = radius * Mathf.Cos(Time.time * speed);

        transform.position = new Vector2(x + defPos.x, y + defPos.y);
        
    }

    //IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(delayTime);
    //}
}
