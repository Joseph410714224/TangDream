using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleManager : MonoBehaviour
{
    public capsule capsule;
    public GameObject puzzleCanvus;
    public GameObject[] tans;
    public Transform[] points;
    public Material outline;
    public bool[] ok;
    public int i = 0;
    private float tanRot = 29.752f;

    void returnToGame()
    {
        if (Input.GetButtonDown("Bagpack"))
        {
            puzzleCanvus.SetActive(false);
            capsule.enabled = true;
            gameObject.SetActive(false);
            
        }
    }

    void puzzleControl()
    {
        if (Input.GetButtonDown("LT"))
        {
            tans[i].GetComponent<Image>().material = null;
            if (i < 6) i++; else i = 0;
            tans[i].GetComponent<Image>().material = outline;
            Debug.Log(i);
        }
        else if (Input.GetButtonDown("RT"))
        {
            tans[i].GetComponent<Image>().material = null;
            if (i > 0) i--; else i = 6;
            tans[i].GetComponent<Image>().material = outline;
            Debug.Log(i);
        }
        if (ok[i] == false)
        {
            tans[i].transform.position += new Vector3(Input.GetAxis("Horizontal") * 3, Input.GetAxis("Vertical") * 3, 0);
            if (Input.GetButton("Lbutton"))
            {
                tans[i].transform.Rotate(0, 0, 0.5f);
            }
            else if (Input.GetButton("Rbutton"))
            {
                tans[i].transform.Rotate(0, 0, -0.5f);
            }
            else if (Input.GetButtonDown("Submit"))
            {
                if (Vector2.Distance(tans[i].transform.localPosition, points[i].localPosition) <= 10 && Mathf.Abs(tans[i].transform.eulerAngles.z - tanRot) <= 10)
                {
                    tans[i].transform.localPosition = points[i].localPosition;
                    tans[i].transform.eulerAngles = new Vector3(0, 0, tanRot);
                    Debug.Log("Confirm");
                    ok[i] = true;
                }
            }
        }
        //Debug.Log("H:" + Input.GetAxis("Horizontal") + "V:" + Input.GetAxis("Vertical"));
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(i);
    }

    // Update is called once per frame
    void Update()
    {
        //returnToGame();
        puzzleControl();
    }
}
