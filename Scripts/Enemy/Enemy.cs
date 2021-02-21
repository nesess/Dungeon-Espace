using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour , IDamageable
{
    [SerializeField]
    protected GameObject Diamond;
    [SerializeField]
    protected float hitRange;
    [SerializeField]
    protected int AgroDistance;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected bool isDead = false;

    public bool isHit = false;
    protected Player player;
    
    protected Vector3 currentTarget;
    public Animator anim;
    protected SpriteRenderer sprite;

    public int Health { get; set; }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        Health = health;
    }

    public virtual void Awake()
    {
        currentTarget = pointB.position;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
       

        if (isDead == true)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Enemy>());
            Destroy(GetComponentInChildren<Agro>());
            Destroy(GetComponentInChildren<BoxCollider2D>());
        }

        if (GameManager.Instance.playerDead == true)
        {
            Destroy(GetComponentInChildren<Agro>());
            isHit = false;
            anim.SetBool("InCombat", false);
            
           
        }
        else
        {
            DistanceWatcher();
        }
        

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }

        if (isHit == false)
        {
            Movement();
        }
        
    }
    
    public virtual void Movement()
    {
        
        if (currentTarget == pointA.position)
        {
            Vector2 theScale = transform.localScale;
            if (theScale.x != 1)
            {
                theScale.x *= -1;
            }
            transform.localScale = theScale;
        }
        else
        {
            Vector2 theScale = transform.localScale;
            if (theScale.x != -1)
            {
                theScale.x *= -1;
            }
            transform.localScale = theScale;
        }

        if (transform.position == pointB.position)
        {
            anim.SetTrigger("Reached");
            currentTarget = pointA.position;
        }
        else if (transform.position == pointA.position)
        {
            anim.SetTrigger("Reached");
            currentTarget = pointB.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }

    public virtual void Damage(int damageAmount)
    {
        Health = Health - damageAmount;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);
        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject myDiamond = Instantiate(Diamond, transform.position, Quaternion.identity);
            Diamond diamond = myDiamond.GetComponent<Diamond>();
            diamond.assingDiamondValue(gems);
            myDiamond.transform.position = new Vector3(myDiamond.transform.position.x, (myDiamond.transform.position.y - 0.4f), 0);
        }

    }

    public virtual void DistanceWatcher()
    {
        if(isHit != false)
        {
            if ((pointA.position.x -0.8)  > player.transform.position.x || (pointB.position.x + 0.8) < player.transform.position.x)
            {
                isHit = false;
                anim.SetBool("InCombat", false);
                return;
            }

            float distance = Vector3.Distance(transform.position, player.transform.position);
            Vector3 direction = player.transform.position - transform.position;
            if (direction.x >= 0)
            {
                Vector2 theScale = transform.localScale;
                if (theScale.x != -1)
                {
                    theScale.x *= -1;
                }
                transform.localScale = theScale;
            }
            else
            {
                Vector2 theScale = transform.localScale;
                if (theScale.x != 1)
                {
                    theScale.x *= -1;
                }
                transform.localScale = theScale;
            }

            if(distance > hitRange)
            {
                anim.SetBool("MoveInCombat", true);
                Vector3 target = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

                if(anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                }
            }
            else
            {
                anim.SetBool("MoveInCombat", false);
            }

            if (distance > AgroDistance)
            {
                isHit = false;
                anim.SetBool("InCombat", false);
            }
        }
    }

}
