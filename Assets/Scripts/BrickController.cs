using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public int hitPoints = 1;
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            hitPoints--;

            if (hitPoints <= 0)
            {
                Destroy(gameObject);
                gameManager.BlockHit();
            }
        }
    }
}

