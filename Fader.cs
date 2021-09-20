using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.SceneManagements
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void FadeOutImmediate()
        {
            while(canvasGroup.alpha<1)
            {
                canvasGroup.alpha  = 1;
            }
        }
        public IEnumerator fadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        public IEnumerator fadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
        // public IEnumerator FadeOutIn()
        // {
        //     yield return fadeOut(3f);
        //     print("faded out");
        //     yield return fadeIn(2f);
        //     print("faded in");
        // }
    }
}

