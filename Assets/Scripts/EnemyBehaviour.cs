using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform playerTrans;
    [SerializeField] float detectionDistance;

    Rigidbody2D enemyRb;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (Mathf.Abs(playerTrans.position.x - transform.position.x) < detectionDistance)
        {
            //Attack
            Debug.Log("die fool");
        }
    }
}
