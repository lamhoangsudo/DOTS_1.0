using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTextPopUp : MonoBehaviour
{
    private float destroyTimer = 1f;
    private void Update()
    {
        transform.position += Vector3.up * 2f * Time.deltaTime;
        destroyTimer -= Time.deltaTime;
        if ( destroyTimer <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
