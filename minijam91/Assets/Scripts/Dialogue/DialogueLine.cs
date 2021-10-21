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
            if (Input.GetMouseButtonDown(0) || (Input.GetKeyDown("return")))
            {
                //Should change that to "speeds up text", it would be cooler
                if(textHolder.text != "* " + input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = "* " + input;
                }
                else
                {
                    finished = true;
                }
            }
        }

        private void ResetLine()
        {
            textHolder = GetComponent<Text>();
            finished = false;
        }
    }
}