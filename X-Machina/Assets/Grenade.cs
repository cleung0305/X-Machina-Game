﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int damage;
    public float radius;
    public float force;
    public float forwardSpeed;
    public float timer;
    public float triggeredTimer;

    public GameObject explosionParticle;
    private GameObject player;

    public AudioSource sound;
    public AudioClip explodeSound;
    public Collider2D grenadeCollider;
    public SpriteRenderer spriteRenderer;

    private bool suicide = false;
    private bool goingLeft = false;
    private bool goingRight = false;
    private bool exploded;
    private Animator animator;
    public Rigidbody2D grenadeBody;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        exploded = false;
        grenadeBody.velocity = transform.right * force;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(countdown());
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(exploded && !sound.isPlaying)
        {
            Destroy(gameObject);
        }
        if (suicide)
        {
            animator.SetBool("Suicide", true);
            if (goingRight)
            {
                transform.position += Vector3.right * Time.deltaTime * forwardSpeed;
            }
            if (goingLeft)
            {
                transform.position += Vector3.left * Time.deltaTime * forwardSpeed;
            }
            StartCoroutine(countdown());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var relativePosition = transform.InverseTransformPoint(collision.transform.position);
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss1")
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (!exploded)
        {
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius);


            Enemy enemy = collider.GetComponent<Enemy>();
            Patrol2 enemyMech = collider.GetComponent<Patrol2>();
            MeleeScript enemyMelee = collider.GetComponent<MeleeScript>();
            flyEnemy enemyfly = collider.GetComponent<flyEnemy>();
            GroundMechScript groundMech = collider.GetComponent<GroundMechScript>();
            BossAI boss = collider.GetComponent<BossAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

            }
            else if (enemyMech != null)
            {
                enemyMech.TakeDamage(damage);
            }
            else if (enemyMelee != null)
            {
                enemyMelee.TakeDamage(damage);
            }
            else if (enemyfly != null)
            {
                enemyfly.TakeDamage(damage);
            }
            else if (groundMech != null)
            {
                groundMech.TakeDamage(damage);
            }
            else if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            //Destroy(gameObject);
            exploded = true;
            sound.clip = explodeSound;
            sound.Play();
            grenadeCollider.enabled = false;
            spriteRenderer.enabled = false;
            player.GetComponent<Weapon>().activeGrenadeCount -= 1;
        }
    }

    public void SuicideBombing()
    {
        suicide = true;
    }
    public void GoRight()
    {
        goingRight = true;
    }
    public void GoLeft()
    {
        goingLeft = true;
    }
    IEnumerator countdown()
    {
        if(suicide)
            yield return new WaitForSecondsRealtime(Mathf.Min(timer, triggeredTimer));
        else
            yield return new WaitForSecondsRealtime(timer);
        Explode();
    }
}
