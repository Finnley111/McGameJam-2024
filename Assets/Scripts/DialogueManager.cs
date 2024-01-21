using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public GameObject startButton;
    public GameObject sprite;
    public Animator characterTalking;

    private Queue<string> sentences;
    private int sentenceNum = 0;
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

        if (sentenceNum == 0) {
            Debug.Log("Sentence 1");
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", true);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 1) {
            Debug.Log("Sentence 2");
            nameText.text = "Harold";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", true);
        }

        if (sentenceNum == 2) {
            Debug.Log("Sentence 3");
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", true);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 3) {
            Debug.Log("Sentence 4");
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", true);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 4) {
            nameText.text = "Harold";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", true);
        }

        if (sentenceNum == 5) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", true);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 6) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", true);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 7) {
            nameText.text = "Harold";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", true);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 8) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", true);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 9) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", true);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 10) {
            nameText.text = "Harold";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", true);
        }

        if (sentenceNum == 11) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", true);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 12) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", true);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 13) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", true);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", false);
        }

        if (sentenceNum == 14) {
            nameText.text = "Harold";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", false);
            characterTalking.SetBool("Harold", true);
        }

        if (sentenceNum == 15) {
            nameText.text = "Byte";
            characterTalking.SetBool("Sprite 1", false);
            characterTalking.SetBool("Sprite 2", true);
            characterTalking.SetBool("Harold", false);
        }

        sentenceNum += 1;
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

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("CurrentScene = " + currentSceneIndex);
    }

    public void NextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("CurrentScene = " + currentSceneIndex);
    }
}
