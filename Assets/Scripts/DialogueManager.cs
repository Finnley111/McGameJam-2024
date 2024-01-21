using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public GameObject startButton;
    public GameObject sprite;
    public Animator characterTalking;

    private Queue<string> sentences;
    void Start()
    {
      sentences = new Queue<string>();  
      characterTalking.SetBool("Sprite 1", false);
      characterTalking.SetBool("Sprite 2", false);
      characterTalking.SetBool("Harold", true);
      sprite.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen?", true);
        sprite.SetActive(true);
        startButton.SetActive(false);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));
    }

    IEnumerator typeSentence (string sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue() {
        animator.SetBool("isOpen?", false);
        sprite.SetActive(false);
    }
}
