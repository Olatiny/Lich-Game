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
        return paths[Random.Range(0, 0)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(spawn))
        {
            name = "Old Path";

            GameObject re = Instantiate(thisPath, new Vector2(transform.position.x, thisPath.transform.GetChild(2).transform.position.y), thisPath.transform.rotation);

            re.name = "New Path";
        }
    }

    void FixedUpdate()
    {
        //Debug.Log("updating!");
        //if (GameManager.Instance.)
        transform.position += new Vector3(0, GameManager.Instance.GetFallSpeed() * Time.fixedDeltaTime, 0);
    }
}
