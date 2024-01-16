using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StickController : MonoBehaviour
{

    void Update()
    {
        StickMovement();
    }

    private void StickMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (transform.rotation.eulerAngles.z >= 50)
            {
                transform.DORotate(new Vector3(50,0,0), 1f).Play();
            }
            else if (transform.rotation.eulerAngles.z < 50)
            {
                transform.DORotate(new Vector3(50, 0, 90), 1f).Play();
            }
            Debug.Log(transform.rotation.eulerAngles);
        }
    }
}
