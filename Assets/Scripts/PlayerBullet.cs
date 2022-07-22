using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    private float lifetime;
    private Animator anim;
    public GameObject enemyGameObject;
    public AnimatorStateInfo animStateInfo;
    public float NTime;

    void Start()
    {
        enemyGameObject = GameObject.Find("Freddy");
        anim = GetComponent<Animator>();
        gameObject.SetActive(true);
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy");
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
        
        if (collision.gameObject == GameObject.Find("Freddy")){
        anim.SetTrigger("explode");
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        NTime = animStateInfo.normalizedTime;
        enemyGameObject.GetComponent<EnemyController>().TakeDamage(1);
        enemyGameObject.GetComponent<EnemyController>().Hurt();
        StartCoroutine(DestroyTimer());
        IEnumerator DestroyTimer ()
        {
            yield return new WaitForSeconds(0.7f);
            Destroy(this.gameObject);
        }
        }
        else Destroy(this.gameObject);

    }

    

}
