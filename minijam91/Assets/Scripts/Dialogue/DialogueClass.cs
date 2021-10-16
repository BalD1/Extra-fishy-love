using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay, string searchedAudio, float delayBetweenLines)
        {
            textHolder.text = "* ";
            textHolder.color = textColor;
            textHolder.font = textFont;

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                //AudioManager.Instance.Play2DSound(searchedAudio);
                yield return new WaitForSeconds(delay);
            }

            //If you want the dialogue to progress on it's own
            //not useful for this project but good to have nonetheless
            if (delayBetweenLines != 0)
            {
                yield return new WaitForSeconds(delayBetweenLines);
                finished = true;
            }
            else //Wait for input to progress
            {
                yield return new WaitUntil(() => (Input.GetMouseButtonDown(0)) || (Input.GetKeyDown("return")));
                finished = true;
            }
        }
    }
}
