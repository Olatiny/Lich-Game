using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    
    public GameObject player;
    public float speed;
    private bool facingLeft;
    public float killTime;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position.x > player.transform.position.x) {
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            facingLeft = true;
        } else {
            facingLeft = false;
        }
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, speed * Time.deltaTime);
        if (gameObject.transform.position.x > player.transform.position.x && !facingLeft) {
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            facingLeft = true;
        } else if (gameObject.transform.position.x < player.transform.position.x && facingLeft) {
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            facingLeft = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Player") {
            Die();
        }
    }
    public void Die() {
        Destroy(gameObject);
    }
    public IEnumerator Timer() {
        yield return new WaitForSeconds(killTime);
        Die();
    }
}
