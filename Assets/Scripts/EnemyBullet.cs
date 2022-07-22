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
    public GameObject playerGameObject;
    public AnimatorStateInfo animStateInfo;
    public float NTime;

    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        anim = GetComponent<Animator>();
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
        anim.SetTrigger("explode");
        animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        NTime = animStateInfo.normalizedTime;
        playerGameObject.GetComponent<CharacterController2D>().TakeDamage(1);
        if (NTime > 1.0f) Destroy(this.gameObject);
    }

}
