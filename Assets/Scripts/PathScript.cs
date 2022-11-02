using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    [SerializeField] GameObject thisPath;
    [SerializeField] GameObject spawn;
    [SerializeField] float offset;

    [SerializeField] List<GameObject> paths;

    public GameObject GetLevelSegment()
    {
        //paths.Count - 1
        int idx = Random.Range(0, paths.Count);
        //idx = paths.Count - 2;
        //Debug.Log("Path: " + idx);
        return paths[idx];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals(spawn.transform.GetChild(0).name))
        {
            //name = "Old Path";

            GameObject newObj = GetLevelSegment();

            //newObj.GetComponent<PathScript>().spawn = spawn;
            //newObj.GetComponent<PathScript>().paths = paths;
            //newObj.GetComponent<PathScript>().offset = offset;

            GameObject re;

            if (name.Contains("Slant"))
            {
                re = Instantiate(newObj, transform.GetChild(1).position, thisPath.transform.rotation);
            }
            else if (name.Contains("Straight")) {
                re = Instantiate(newObj, transform.GetChild(2).position, thisPath.transform.rotation);
            }
            else
            {
                re = Instantiate(newObj, transform.GetChild(6).gameObject.GetComponent<PathTransitionScript>().WhichConnector().transform.position, thisPath.transform.rotation);
            }

            re.GetComponent<PathScript>().offset = offset;
            //Debug.Log(re.name);

            //if (re.name.Contains("Path-2(Clone)"))
            //{
            //    //Debug.Log("p2");
            //    re.GetComponent<PathScript>().offset += 18.13f;
            //} if (re.name.Contains("Path-1(Clone)"))
            //{
            //    re.GetComponent<PathScript>().offset = 0;
            //}

            //re.name = "New Path";
        }
    }

    void FixedUpdate()
    {
        //Debug.Log("updating!");
        //if (GameManager.Instance.)
        transform.position += new Vector3(0, GameManager.Instance.GetFallSpeed() * Time.fixedDeltaTime, 0);
    }
}
