using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Import TextMeshPro namespace

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // TextMeshPro component voor de dialoog
    public GameObject dialogueBox; // Het dialoogvenster
    public Button nextButton; // knop om naar volgende zin te gaan
    public TextMeshProUGUI nextButtonText; // tekst op de knop
    public GameObject interactionText; // Tekst die aangeeft dat je de dialogue kan starten
    public string[] dialogueLines; // De dialoogregels
    public string[] buttonLabels;

    public float interactionDistance = 50f; // Afstand waarbinnen interactie mogelijk is
    private int currentSentenceIndex;
    public bool playerInRange = false;
    public bool isDialogueActive = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        nextButton.gameObject.SetActive(false);
        interactionText.gameObject.SetActive(false); // Verberg interactietekst bij start
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !isDialogueActive)
        {
            interactionText.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                StartDialogue();
            }
        }
    }

    void StartDialogue()
    {
        dialogueBox.SetActive(true);
        nextButton.gameObject.SetActive(true);
        print(nextButton.gameObject.name);
        interactionText.gameObject.SetActive(false); // Verberg interactietekst zodra dialoog start
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(DisplayNextSentence);
        isDialogueActive = true;
        LockPlayerMovement(); // beweging uitschakelen
        currentSentenceIndex = 0; // zet de index terug naar 0 zodat de dialoog opnieuw werkt

        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (currentSentenceIndex >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueLines[currentSentenceIndex];

        // Wijzig de knoptekst indien mogelijk
        if (currentSentenceIndex < buttonLabels.Length)
        {
            nextButtonText.text = buttonLabels[currentSentenceIndex];
        }
        else
        {
            nextButtonText.text = "Volgende"; // Standaardtekst als er geen label is
        }

        currentSentenceIndex++;
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        nextButton.gameObject.SetActive(false);
        isDialogueActive = false;
        UnlockPlayerMovement(); // beweging inschakelen

        if (!playerInRange)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MC")) //gameObject.CompareTag("NPC")
        {
            playerInRange = true;
            if (!isDialogueActive)
            {
                interactionText.gameObject.SetActive(true); // Toon de interactietekst
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MC"))
        {
            playerInRange = false;
        }
        interactionText.gameObject.SetActive(false);
    }

    // 🔒 Speler blokkeren door de tijd te pauzeren
    void LockPlayerMovement()
    {
        Time.timeScale = 0f;
    }

    // 🔓 Speler weer laten bewegen
    void UnlockPlayerMovement()
    {
        Time.timeScale = 1f;
    }
}
