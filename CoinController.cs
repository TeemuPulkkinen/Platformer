using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    //rotation speed for the coin
    public float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // angle of rotation velocity = distance / time --> distance = velocity * time
        float angleRot = rotationSpeed * Time.deltaTime;

        // rotate coin
        transform.Rotate(Vector3.up * angleRot, Space.World);
    }
}
