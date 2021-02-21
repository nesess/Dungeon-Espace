using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{


    public override void Init()
    {
        base.Init();

    }

    public override void Damage(int damageAmount)
    {
        Health = Health - damageAmount;
        if (Health < 1)
        {
            base.isDead = true;
            anim.SetTrigger("Death");
            GameObject myDiamond = Instantiate(base.Diamond, transform.position, Quaternion.identity);
            Diamond diamond = myDiamond.GetComponent<Diamond>();
            diamond.assingDiamondValue(gems);
            myDiamond.transform.position = new Vector3(myDiamond.transform.position.x,(myDiamond.transform.position.y - 0.2f), 0);
        }
    }

    public override void Update()
    {
        if (base.isDead == true)
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Spider>());
            Destroy(GetComponentInChildren<SpiderAnimationEvent>());
        }

    }
    


    public override void Movement()
    {
        
    }


    public override void DistanceWatcher()
    {
        
    }
}
