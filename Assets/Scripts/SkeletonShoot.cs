using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShoot : MonoBehaviour
{
    public float shootSpeed;
    private Transform shootPos;
    public GameObject Bone;
    private float time;
    public float averageTime = 1;
    public float timeVariance = 0.5f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            //player = GameManager.Instance.GetPlayer();
        }
        if (gameObject.transform.position.x > player.transform.position.x) {
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            shootSpeed *= -1;
        }   
        shootPos = gameObject.transform;
        time = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time <= 0) {
            time = Random.Range(averageTime - timeVariance, averageTime + timeVariance);
            Shoot();
        }
        time -= Time.fixedDeltaTime;
    }
    void Shoot() {
        if (GameManager.Instance.CanMove())
        {
            GameObject newBone = Instantiate(Bone, shootPos.position, Quaternion.identity);
            newBone.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            newBone.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, 0f);
        }
    }
}
