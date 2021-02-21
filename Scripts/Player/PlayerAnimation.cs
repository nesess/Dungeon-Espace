using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _swordAnimation;
    [SerializeField]
    private LayerMask _groundLayer;
    private bool onAir = false;
    private bool canJump = true;


    // Start is called before the first frame update
    void Start()
    {
        
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();

    }

    void Update()
    {
        if(onAir)
        {
            if(isGrounded())
            {
                _anim.SetBool("Jump1", false);
                onAir = false;
                _anim.SetBool("Jump2", true);
                StartCoroutine(waitForJump3());

            }
        }
            
    }


    public void Run(float move)
    {
        _anim.SetFloat("Move",Mathf.Abs(move));
    }

   
    public void Jump()
    {
        canJump = false;
        _anim.SetBool("Jump1", true);
        StartCoroutine(waitForJump2());
        
    }
    
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, _groundLayer);
        
        if (hitInfo.collider != null)
        {
            return true;
        }
        return false;
    }

    IEnumerator waitForJump2()
    {
        yield return new WaitForSeconds(0.3f);
        onAir = true;
    }

    IEnumerator waitForJump3()
    {
        yield return new WaitForSeconds(0.3f);
        _anim.SetBool("Jump2", false);
        canJump = true;
    }

    public bool getCanJump()
    {
        return canJump;
    }

    public void regAttack()
    {
        _anim.SetTrigger("regAttack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }


}
