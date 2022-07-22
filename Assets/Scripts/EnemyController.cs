using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Transform player;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 0.5f;
    private float nextFireTime;
    private Rigidbody2D rb;
    public GameObject bullet;
    public GameObject bulletEmitter;// Start is called before the first frame update

    private Animator anim;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Hurt()
    {
        anim.SetTrigger("hit");
    }

    void Update()
    {
        //Seguir al player si esta dentro del lineof sight y parar de seguir cuando entre en shooting range
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= 5)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, -1 * speed * Time.deltaTime);
        }

        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time && distanceFromPlayer > 4)
        {
            anim.SetTrigger("attack");
            Instantiate(bullet, bulletEmitter.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

    }

    //Visualizacion del range de line of sight y shooting range
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}