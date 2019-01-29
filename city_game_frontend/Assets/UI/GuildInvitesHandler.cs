using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildInvitesHandler : MonoBehaviour {

    private List<string> playersInParty = null;

    public GameObject itemPrefab, invitesGrid;
    private int itemCount = 1;

    public void getPlayersInPartyListFromServer()
    {
        //TODO
        //get info from the server
        playersInParty = new List<string>();

        playersInParty.Add("Kacper");
        playersInParty.Add("Melchior");
        playersInParty.Add("Bulbazaur");

        itemCount = playersInParty.Count;
    }

    public void invitePlayerByName(InputField inputField)
    {
        if (inputField.text != "")
        {
            playersInParty.Add(inputField.text);
            updatePlayersInPartyList();
        }
        //TODO
        //send info to the server
    }

    public void kickThePlayerOutOfTheParty(GameObject memberTile, Text playerName)
    {
        playersInParty.Remove(playerName.text);
        GameObject.Destroy(memberTile);
        updatePlayersInPartyList();
    }

    public void updatePlayersInPartyList()
    {

        itemCount = playersInParty.Count;
        playersInParty.Sort();

        foreach (Transform g in invitesGrid.transform)
            GameObject.Destroy(g.gameObject);

        RectTransform rowTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform invitesGridTransform = invitesGrid.GetComponent<RectTransform>();

        float width = invitesGridTransform.rect.width;
        float ratio = width / rowTransform.rect.width;
        float height = rowTransform.rect.height * ratio;

        float scrollHeight = height * itemCount;
        invitesGridTransform.offsetMin = new Vector2(invitesGridTransform.offsetMin.x, -scrollHeight / 2);
        invitesGridTransform.offsetMax = new Vector2(invitesGridTransform.offsetMax.x, scrollHeight / 2);

        for (int i = 0; i < itemCount; i++)
        {

            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = playersInParty[i] + " invite tile";
            newItem.transform.parent = invitesGrid.transform;

            Text newItemsText = newItem.GetComponentInChildren<Text>();
            newItemsText.text = playersInParty[i];

            Button newItemsButton = newItem.transform.Find("KickButton").GetComponent<Button>();
            newItemsButton.onClick.AddListener(() => kickThePlayerOutOfTheParty(newItem, newItemsText));

            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -invitesGridTransform.rect.width / 2 + width;
            float y = invitesGridTransform.rect.height / 2 - height;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }


    }


    void Start()
    {
        getPlayersInPartyListFromServer();
        
        RectTransform rowTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform invitesGridTransform = invitesGrid.GetComponent<RectTransform>();
        
        float width = invitesGridTransform.rect.width;
        float ratio = width / rowTransform.rect.width;
        float height = rowTransform.rect.height * ratio;
        
        float scrollHeight = height * itemCount;
        invitesGridTransform.offsetMin = new Vector2(invitesGridTransform.offsetMin.x, -scrollHeight / 2);
        invitesGridTransform.offsetMax = new Vector2(invitesGridTransform.offsetMax.x, scrollHeight / 2);
        
        for (int i = 0; i < itemCount; i++)
        {
            
            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = playersInParty[i] + " invite tile";
            newItem.transform.parent = invitesGrid.transform;

            Text newItemsText = newItem.GetComponentInChildren<Text>();
            newItemsText.text = playersInParty[i];

            Button newItemsButton = newItem.transform.Find("KickButton").GetComponent<Button>();
            newItemsButton.onClick.AddListener(() => kickThePlayerOutOfTheParty(newItem, newItemsText));

            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -invitesGridTransform.rect.width / 2 + width;
            float y = invitesGridTransform.rect.height / 2 - height;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }


    }
}
