using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueClass
    {
        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Audio")]
        [SerializeField] private string searchedAudio;

        [Header("Time Options")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBetweenLines = 0;

        private Text textHolder;
        private IEnumerator lineAppear;

        private void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input, textHolder, textColor, textFont, delay, searchedAudio, delayBetweenLines);
            StartCoroutine(lineAppear);
        }

        private void Update()
        {

        }

        private void ResetLine()
        {
            textHolder = GetComponent<Text>();
            finished = false;
        }
    }
}