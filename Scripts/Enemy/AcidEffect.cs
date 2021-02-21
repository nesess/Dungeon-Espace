using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2;

    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    
    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        

        if (other.tag == "Player")
        {
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
        }

       
    }

}
