using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    
    private PlayerAnimation _anim;
    private bool _attacking = false;
    private Rigidbody2D _rigidbody;
    private float move;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _jumpForce;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private int health = 4;
    [SerializeField]
    public int Diamonds = 0;

    private BoxCollider2D boxCollider;
    public Animator anim;
    

    public int Health { get; set; }

    void Start()
    {
        Health = health;
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        if (GameManager.Instance.playerDead == true)
        {
           
            return;
        }
        Movement();
        Attack();
        
        
    }

    void Movement()
    {

        move = CrossPlatformInputManager.GetAxis("Horizontal");
        //move = Input.GetAxisRaw("Horizontal");
            if (move >= 1)
            {
                Vector2 theScale = transform.localScale;
                if (theScale.x != 1)
                {
                    theScale.x *= -1;
                }
                transform.localScale = theScale;
            }

            if (move <= -1)
            {
                Vector2 theScale = transform.localScale;
                if (theScale.x != -1)
                {
                    theScale.x *= -1;
                }
                transform.localScale = theScale;
            }
        if (_attacking == false)
        {

            if (Input.GetKeyDown(KeyCode.Space) == true || CrossPlatformInputManager.GetButtonDown("B_Button") && isGrounded() && _anim.getCanJump() && !anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
                _anim.Jump();

            }
        }
        _rigidbody.velocity = new Vector2(move * _speed, _rigidbody.velocity.y);
            _anim.Run(move);
        
    }
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, _groundLayer);
        if (hitInfo.collider != null)
        { 
            return true;
        }
        return false;
    }

    void Attack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && isGrounded() && _attacking == false)
        {
            _attacking = true;
            _anim.regAttack();
            StartCoroutine(waitForAttack());
        }
    }

    IEnumerator waitForAttack()
    {
        yield return new WaitForSeconds(0.6f);
        _attacking = false;
    }

    public void Damage(int damageAmount)
    {
        Health = Health - damageAmount;
        UIManager.Instance.UpdateHealthHud(Health);
        if(Health > 0)
        {
            anim.SetTrigger("Hit");
        }
        if (Health < 1)
        {
            GameManager.Instance.playerDead = true;
            Destroy(GetComponent<BoxCollider2D>());
            _rigidbody.gravityScale = 0;
            anim.SetTrigger("Death"); 
        }
    }

    public void  increaseDiamondAmount(int amount)
    {
        Diamonds += amount;
        UIManager.Instance.UpdateGemHud(Diamonds);
    }


    public int getGemCount()
    {
        return Diamonds;
    }

    public void decreaseGem(int amount)
    {
        Diamonds -= amount;
    }
}