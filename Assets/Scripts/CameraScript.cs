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

    public IEnumerator StartRotate(Quaternion finalRotation, float rotateTime, bool includePlayer = true)
    {
        Debug.Log("start rotation for " + rotateTime);
        float t = 0f;
        float startRot = transform.eulerAngles.z;
        float finalRot = finalRotation.z;
        while (t < rotateTime)
        {
            
            t += Time.deltaTime;
            //Debug.Log(t);
            float zRot = Mathf.LerpAngle(startRot, finalRot, t / rotateTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRot);
            if(includePlayer){
                player.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRot);
            }
            //Debug.Log("rotating " + t + ", " + (t / rotateTime) + ", " + zRot);
            yield return null;
        }
        Debug.Log("end rotation");
    }
    public IEnumerator StartMove(Vector3 goalPos, float moveTime, bool includePlayer = true)
    {
        Debug.Log("start moving for " + moveTime);
        float t = 0f;
        Vector3 startPos = player.transform.position;
        while (t < moveTime)
        {
            
            t += Time.deltaTime;
            //Debug.Log(t);
            Vector3 currentPos = Querp(startPos, goalPos, t / moveTime);
            player.transform.position = currentPos;
            //Debug.Log("rotating " + t + ", " + (t / rotateTime) + ", " + zRot);
            yield return null;
        }
        Debug.Log("end moving at " + player.transform.position);
    }

    public void SetRotation(Quaternion newRotation, bool includePlayer = true){
        transform.rotation = newRotation;
        if(includePlayer){
            player.transform.rotation = newRotation;
        }
    }

    private Vector3 Querp(Vector3 start, Vector3 end, float t){
        if(t > .998f){
            return end;
        }
        Vector3 movePath = end-start;
        float result = (1f/.0313f)*(-(Mathf.Pow(t,4)/4)+(Mathf.Pow(t,3)/2)-(9*Mathf.Pow(t,2)/32)+(t/16));
        Vector3 resultPos = movePath * result;
        Vector3 rand = new Vector3(Random.Range(-.01f,.01f), Random.Range(-.01f,.01f), 0);
        //Debug.Log("start: " + start + "\nend: " + end + "resultPos: " + resultPos + "");
        return start + resultPos + rand;
    }

}
