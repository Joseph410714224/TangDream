using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (currentHealth > 1)
        {
            if (col.collider.tag == "board")
            {
                TakeDamage(1);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    [SerializeField]
    public Transform player;
    public Transform atkpoint;
    public bool atk = true;

    NavMeshAgent nav;

    public bool col;
    public float dis;

    public GameObject[] board;
    public int i;

    public int maxHealth = 2;
    public int currentHealth;
    public healthBar healthBar;

    void Attack()
    {
        if (atk)
        {
            i = Random.Range(0, 3);
            board[i].SetActive(true);
            board[i].transform.parent = null;
            board[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 3, -8);
            Debug.Log("shoot");
            atk = !atk;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void SetDestination()
    {
        if (dis <= 0.8f || dis >= 8)
        {
            col = true;
            nav.enabled = false;
        }
        else
        {
            nav.enabled = true;
            nav.SetDestination(player.position);
        }
    }

    void SetDistance()
    {
        dis = Vector3.Distance(gameObject.transform.position, player.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        col = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();
        SetDistance();
        Attack();
    }
}
