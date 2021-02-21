using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private int diamondValue = 1;

    public void assingDiamondValue(int value)
    {
        diamondValue = value;
        if(value >2 && value < 5)
        {
            mSize();
        }
        else if(value >4)
        {
            lSize();
        }
    }
    
    public void mSize()
    {
        transform.localScale += new Vector3(1,1,0);
    }

    public void lSize()
    {
        transform.localScale += new Vector3(1.5f, 1.5f, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.increaseDiamondAmount(diamondValue);
                Destroy(this.gameObject);
            }
        }
    }

}
