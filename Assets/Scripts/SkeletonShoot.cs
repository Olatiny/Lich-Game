using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShoot : MonoBehaviour
{
    public float shootSpeed;
    public Transform shootPos;
    public GameObject Bone;
    public float time;
    public float averageTime = 1;
    public float timeVariance = 0.5f;
    public float elapsedTime = 0;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.position.x > player.transform.position.x) {
            gameObject.transform.Rotate(0, 180, 0, Space.Self);
            shootSpeed *= -1;
        }
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
        GameObject newBone = Instantiate(Bone, shootPos.position, Quaternion.identity);
        newBone.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * Time.fixedDeltaTime, 0f);
        StartCoroutine(newBone.GetComponent<BoneScript>().Timer());
    }
}