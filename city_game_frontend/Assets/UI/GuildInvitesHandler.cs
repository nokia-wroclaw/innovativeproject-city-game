using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildInvitesHandler : MonoBehaviour {

    public GameObject itemPrefab,guildPanel;
    private List<string> playersInParty = null;

    public void invitePlayerByName()
    {
        //TODO
    }

    public void updateListView()
    {



    }
    
	void Start () {

        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform containerRectTransform = guildPanel.GetComponent<RectTransform>();

        float ratio = containerRectTransform.rect.width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height * ratio;

        float scrollHeight = height * playersInParty.Count;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

        //TODO getting list of players in paty from the server
        playersInParty.Add("Kacper");
        playersInParty.Add("Melchior");
        playersInParty.Add("Bulbazaur");

        for (int i = 0; i < playersInParty.Count; i++)
        {
            GameObject newlyCreatedItem = Instantiate(itemPrefab) as GameObject;
            newlyCreatedItem.name = playersInParty[i] + " " + i;

            RectTransform rectTransform = newlyCreatedItem.GetComponent<RectTransform>();

            newlyCreatedItem.transform.position = new Vector3(transform.position.x, transform.position.y + rectTransform.rect.height * (i + 1), transform.position.z);
            newlyCreatedItem.transform.parent = containerRectTransform.transform;
        }
    }
}
