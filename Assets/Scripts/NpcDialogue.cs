using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;

    public bool readyToSpeak;
    public bool startDialogue;

    private Coroutine currentDialogueRoutine;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && readyToSpeak)
        {
            if (!startDialogue)
            {
                StartDialogue();
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueNpc.Length)
        {
            if (currentDialogueRoutine != null)
            {
                StopCoroutine(currentDialogueRoutine);
            }
            currentDialogueRoutine = StartCoroutine(ShowDialogue());
        }
        else
        {
            dialoguePanel.SetActive(false);
            startDialogue = false;
            dialogueIndex = 0;
        }
    }

    void StartDialogue()
    {
        dialogueIndex = 0;
        startDialogue = true;
        dialoguePanel.SetActive(true);
        currentDialogueRoutine = StartCoroutine(ShowDialogue());
    }
    //pra pegar e passar o dialogo por cada letra com intervalo de tempo
    IEnumerator ShowDialogue()
    {
        dialogueText.text = "";

        foreach (char letter in dialogueNpc[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //aqui é pra detectar quando entre no triger e startar o dialogo
        if (col.gameObject.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {   
        //só pra fechar quando o player não tiver mais na área do trigger
        if (col.gameObject.CompareTag("Player"))
        {
            readyToSpeak = false;
        }
    }
}
