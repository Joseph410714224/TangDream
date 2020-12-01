using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class capsule : MonoBehaviour
{
    public Transform cam;
    public Transform realCam;
    public Vector3 mov;
    public Quaternion camview;
    public GameObject puzzleCanvus;
    public GameObject puzzleManager;

    public enemyManager enemyManager;

    public Transform hand;
    public Vector3 throwAngle;
    public bool hold = false;
    public int throwSpd;

    public int MoveSpd;
    public int RotSpd;
    public bool landed;

    public GameObject[] icons;

    public int maxHealth = 10;
    public int currentHealth;
    public healthBar healthBar;

    private Rigidbody rgdb;

    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "board")
        {
            TakeDamage(1);
        }
        Debug.Log("landed");
        landed = true;
    }

    void camRot()
    {
        camview.y += Input.GetAxis("RJH") * RotSpd * Time.deltaTime;
        camview.x += Input.GetAxis("RJV") * RotSpd * (-1) * Time.deltaTime;

        camview.x = Mathf.Clamp(camview.x, -10, 30);
        cam.localRotation = Quaternion.Euler(camview.x, camview.y, camview.z);

        //Debug.Log(camview.x);
    }

    private Vector3 RotWithView()
    {
        if(cam != null)
        {
            Vector3 dir = cam.TransformDirection(mov);
            dir.Set(dir.x, 0, dir.z);
            return dir.normalized * mov.magnitude;
        }
        else
        {
            cam = Camera.main.transform;
            return mov;
        }
    }

    void ThrowAngle()
    {
        throwAngle = hand.position - realCam.position + new Vector3(0, 3, 0);
        throwAngle.Normalize();
    }

    private Vector3 PlayerControl()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxis("LJH") * MoveSpd * Time.deltaTime;
        dir.z = -Input.GetAxis("LJV") * MoveSpd * Time.deltaTime;

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }

    void Move()
    {
        transform.position += mov;
        if (Input.GetButton("Jump") && landed == true)
        {
            rgdb.velocity = new Vector3(0, 5, 0);
            Debug.Log(Input.GetButton("Jump"));
            landed = false;
            Debug.Log(Physics.gravity);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Catch_and_Counter()
    {
        if (Vector3.Distance(enemyManager.board[enemyManager.i].transform.position, transform.position) <= 2 && Input.GetButtonDown("Rbutton"))
        {
            if (!hold)
            {
                enemyManager.board[enemyManager.i].transform.position = hand.position;
                enemyManager.board[enemyManager.i].transform.parent = hand;
                enemyManager.board[enemyManager.i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                enemyManager.board[enemyManager.i].GetComponent<Rigidbody>().useGravity = false;
                enemyManager.board[enemyManager.i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                icons[enemyManager.i].SetActive(true);
                Debug.Log(enemyManager.board[enemyManager.i].gameObject.name);
            }
            else
            {
                enemyManager.board[enemyManager.i].transform.parent = null;
                enemyManager.board[enemyManager.i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                enemyManager.board[enemyManager.i].GetComponent<Rigidbody>().velocity = throwAngle * throwSpd;
                enemyManager.board[enemyManager.i].GetComponent<Rigidbody>().useGravity = true;
            }
            hold = !hold;
        }
    }

    void Bagpack()
    {
        if (Input.GetButtonDown("Bagpack"))
        {
            if (puzzleCanvus.activeSelf == false)
            {
                Debug.Log("Bag");
                puzzleCanvus.SetActive(true);
                puzzleManager.SetActive(true);
                enabled = false;
            }
            else
            {
                puzzleCanvus.SetActive(false);
                puzzleManager.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camview = cam.localRotation;

        rgdb = GetComponent<Rigidbody>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        landed = true;
    }

    // Update is called once per frame
    void Update()
    {
        mov = PlayerControl();
        mov = RotWithView();
        Move();
        camRot();
        Bagpack();
        Catch_and_Counter();
        ThrowAngle();
    }
}
