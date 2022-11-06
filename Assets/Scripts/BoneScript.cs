using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
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
        gameObject.transform.Rotate(0, 0, 5, Space.Self);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Die();
    }
    public void Die() {
        Destroy(gameObject);
    }
}
