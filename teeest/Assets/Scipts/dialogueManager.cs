using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Image dialogueImage;
    public Sprite dialogue1, dialogue2;

    public Animator animator;
    public float textWaitTime;

    private Queue<string> sentences;

    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsOpen", true);
            nameText.text = dialogue.name;
            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }
    }

    void DisplayNextSentence()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            else if (sentences.Count == 2)
            {
                dialogueImage.sprite = dialogue2;
                nameText.rectTransform.localPosition = new Vector3(nameText.rectTransform.localPosition.x * -1, nameText.rectTransform.localPosition.y, 0);
                nameText.text = "波波";
            }
            string sentence = sentences.Dequeue();
            // make sure if type sentence if all ready running, it was stopped and start a new one
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) //convert string to char array
        {
            dialogueText.text += letter;
            // wait for a single frame
            yield return new WaitForSeconds(textWaitTime);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    private void Update()
    {
        DisplayNextSentence();
        StartDialogue(dialogue);
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsOpen", false);
        }
    }
}
