using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBoard : MonoBehaviour
{
    public enemyManager enemyManager;
    public capsule capsule;

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Enemy" || col.collider.tag == "Floor" || col.collider.tag == "Player")
        {
            Debug.Log("Back");
            transform.position = enemyManager.atkpoint.transform.position;
            gameObject.SetActive(false);
            capsule.icons[enemyManager.i].SetActive(false);
            enemyManager.atk = !enemyManager.atk;
        }
        else if (col.collider.tag == "wall")
        {
            Physics.IgnoreCollision(col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
