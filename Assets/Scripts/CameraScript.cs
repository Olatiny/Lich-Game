using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position;
    }

    public IEnumerator StartRotate(Quaternion finalRotation, float rotateTime)
    {
        Debug.Log("start rotation");
        float t = 0f;
        float startRot = transform.eulerAngles.z;
        float finalRot = finalRotation.z;
        while (t < rotateTime)
        {
            
            t += Time.deltaTime;
            float zRot = Mathf.LerpAngle(startRot, finalRot, t / rotateTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRot);
            Debug.Log("rotating " + t + ", " + (t / rotateTime) + ", " + zRot);
            yield return null;
        }
    }

    public void SetRotation(Quaternion newRotation){
        transform.rotation = newRotation;
    }
}
