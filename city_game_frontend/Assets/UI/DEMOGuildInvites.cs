using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DEMOGuildInvites : MonoBehaviour
{
    public GameObject itemPrefab;
    public int itemCount = 10;

    void Start()
    {
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();

        //calculate the width and height of each child item.
        float width = containerRectTransform.rect.width;
        float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height * ratio;
        int rowCount = itemCount;
        if (itemCount % rowCount > 0)
            rowCount++;

        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
        
        for (int i = 0; i < itemCount; i++)
        {

            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = gameObject.name + " item at (" + i +")";
            newItem.transform.parent = gameObject.transform;

            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -containerRectTransform.rect.width / 2 + width * (i % 1);
            float y = containerRectTransform.rect.height / 2 - height;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }
    }
}