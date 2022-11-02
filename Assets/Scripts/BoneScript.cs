using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
    public float killTime;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position.x > player.transform.position.x) {
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject) {
            gameObject.transform.Rotate(0, 0, 5, Space.Self);
        }
    }

    public void OnCollisionEnter2D(Collision2D other) {
        Die();
    }
    public void Die() {
        if (gameObject) {
            Destroy(gameObject);
        }
    }
    public IEnumerator Timer() {
        yield return new WaitForSeconds(killTime);
        Die();
    }
}
