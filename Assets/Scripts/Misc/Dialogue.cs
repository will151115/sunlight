using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("UI References")]
    public GameObject textBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject prompt;

    [Header("Dialogue Settings")]
    public string characterName;
    [TextArea(3, 10)]
    public string[] lines;
    public float textSpeed = 0.05f;

    [Header("References")]
    public GameObject player;

    private int index;
    private bool started;
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && !started && Input.GetKeyDown(KeyCode.Return))
        {
            StartDialogue();
        }

        if (started && Input.GetKeyDown(KeyCode.Return))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        started = true;
        player.GetComponent<PlayerMovement>().enabled = false;

        prompt.SetActive(false);
        textBox.SetActive(true);

        nameText.text = characterName;
        dialogueText.text = string.Empty;
        index = 0;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        started = false;
        textBox.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            prompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            prompt.SetActive(false);

            if (started)
            {
                EndDialogue();
            }
        }
    }
}
