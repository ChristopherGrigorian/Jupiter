using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipBook : MonoBehaviour
{
    [SerializeField] private Image changingImage;
    [SerializeField] private List<Sprite> flipbookImages;

    private void OnEnable()
    {
        StartCoroutine(FlipbookRoutine(flipbookImages));
    }


    private IEnumerator FlipbookRoutine(List<Sprite> images)
    {
        int index = 0;
        while (true)
        {
            if (images == null || images.Count == 0)
            {
                changingImage.sprite = null;
                yield break;
            }

            changingImage.sprite = images[index];
            index = (index + 1) % images.Count;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
