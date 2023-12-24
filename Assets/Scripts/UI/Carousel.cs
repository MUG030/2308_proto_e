using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] ScrollRect scrollRect;

    private RectTransform contentTransform;
    private float itemWidth;
    private int currentItemIndex = 0;

    private void Start()
    {
        contentTransform = scrollRect.content;

        GridLayoutGroup gridLayout = contentTransform.GetComponent<GridLayoutGroup>();
        itemWidth = gridLayout.cellSize.x + gridLayout.spacing.x;

        leftButton.onClick.AddListener(ScrollLeft);
        rightButton.onClick.AddListener(ScrollRight);
    }

    private void ScrollLeft()
    {
        currentItemIndex--;
        if (currentItemIndex < 0)
            currentItemIndex = 0;

        Vector2 targetPosition = new Vector2(-currentItemIndex * itemWidth, 0f);
        scrollRect.content.anchoredPosition = targetPosition;
    }

    private void ScrollRight()
    {
        currentItemIndex++;
        if (currentItemIndex >= contentTransform.childCount)
            currentItemIndex = contentTransform.childCount - 1;

        Vector2 targetPosition = new Vector2(-currentItemIndex * itemWidth, 0f);
        scrollRect.content.anchoredPosition = targetPosition;
    }
}
