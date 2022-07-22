using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;
    public GameObject other; 
    // Start is called before the first frame update
    void Start()
    {
        other = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        gameObject.SetActive(true);
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        StartCoroutine(SelfDestruct());
        IEnumerator SelfDestruct()
        {
            yield return new WaitForSeconds(5f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //anim.SetTrigger("explode");
        other.GetComponent<CharacterController2D>().TakeDamage(5);
        Destroy (this.gameObject);

    }

}
