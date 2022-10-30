using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
    public GameObject dieEffect;
    public float killTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public void Die() {
        if (dieEffect != null) {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    public IEnumerator Timer() {
        yield return new WaitForSeconds(killTime);
        Die();
    }
}
