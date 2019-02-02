using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildInvitesHandler : MonoBehaviour {

    public static GuildInvitesHandler Instance;

    private List<string> playersInParty = null;
    private List<string> invitesPending = null;

    public GameObject guildPanel, invitationsPanel;
    public GameObject itemPrefab, invitationTile, invitesGrid, membersGrid;
    private int itemCount;

    public bool wasAlreadyStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    public void getPlayersInPartyListFromServer()
    {
        GuildActions.Instance.getGuildData();
        playersInParty = GuildDataManager.Instance.guildData.members;

        itemCount = GuildDataManager.Instance.guildData.members_count;
    }

    public void getInvitationsListFromServer()
    {
        List<GuildInvite> gi = PlayerDataManager.Instance.currentPlayerData.invites;
        invitesPending = new List<string>();
        for (int i=0;i<gi.Count;i++)
        {
            invitesPending.Add(gi[i].guild_name);
        }
        itemCount = invitesPending.Count;
    }

    public void acceptThisInvite(Text guildName)
    {
        int inviteId = PlayerDataManager.Instance.currentPlayerData.invites.Find(x => x.guild_name == guildName.text).invite_id;
        GuildActions.Instance.acceptInvite(inviteId);
        startGuildPanel();
    }

    public void invitePlayerByName(InputField inputField)
    {
        if (inputField.text != "")
        {
            GuildActions.Instance.inviteToGuild(inputField.text);
            playersInParty.Add(inputField.text);
            updatePlayersInPartyList();
        }
    }

    public void kickThePlayerOutOfTheParty(GameObject memberTile, Text playerName)
    {
        GuildActions.Instance.sendKickRequest(playerName.text);
        playersInParty.Remove(playerName.text);
        GameObject.Destroy(memberTile);
        updatePlayersInPartyList();
    }

    public void updatePlayersInPartyList()
    {
        getPlayersInPartyListFromServer();

        playersInParty.Sort();

        foreach (Transform g in membersGrid.transform)
            GameObject.Destroy(g.gameObject);

        RectTransform rowTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform invitesGridTransform = membersGrid.GetComponent<RectTransform>();

        float width = invitesGridTransform.rect.width;
        float ratio = width / rowTransform.rect.width;
        float height = rowTransform.rect.height * ratio;

        float scrollHeight = height * itemCount;
        invitesGridTransform.offsetMin = new Vector2(invitesGridTransform.offsetMin.x, -scrollHeight / 2);
        invitesGridTransform.offsetMax = new Vector2(invitesGridTransform.offsetMax.x, scrollHeight / 2);

        for (int i = 0; i < itemCount; i++)
        {

            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = playersInParty[i] + " member tile";
            newItem.transform.parent = membersGrid.transform;
            newItem.SetActive(true);

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

    public void updateInvitesList()
    {
        getInvitationsListFromServer();

        invitesPending.Sort();

        foreach (Transform g in invitesGrid.transform)
            GameObject.Destroy(g.gameObject);

        RectTransform rowTransform = invitationTile.GetComponent<RectTransform>();
        RectTransform invitesGridTransform = invitesGrid.GetComponent<RectTransform>();

        float width = invitesGridTransform.rect.width;
        float ratio = width / rowTransform.rect.width;
        float height = rowTransform.rect.height * ratio;

        float scrollHeight = height * itemCount;
        invitesGridTransform.offsetMin = new Vector2(invitesGridTransform.offsetMin.x, -scrollHeight / 2);
        invitesGridTransform.offsetMax = new Vector2(invitesGridTransform.offsetMax.x, scrollHeight / 2);

        for (int i = 0; i < itemCount; i++)
        {
            GameObject newItem = Instantiate(invitationTile) as GameObject;
            newItem.name = invitesPending[i] + " invite tile";
            newItem.transform.parent = invitesGrid.transform;
            newItem.SetActive(true);

            Text newItemsText = newItem.GetComponentInChildren<Text>();
            newItemsText.text = invitesPending[i];

            Button newItemsButton = newItem.transform.Find("AcceptButton").GetComponent<Button>();
            newItemsButton.onClick.AddListener(() => acceptThisInvite(newItemsText));

            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -invitesGridTransform.rect.width / 2 + width;
            float y = invitesGridTransform.rect.height / 2 - height;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }


    }

    public void startGuildPanel()
    {
        invitationsPanel.SetActive(false);
        guildPanel.SetActive(true);

        getPlayersInPartyListFromServer();

        foreach (Transform g in membersGrid.transform)
            GameObject.Destroy(g.gameObject);

        RectTransform rowTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform invitesGridTransform = membersGrid.GetComponent<RectTransform>();

        float width = invitesGridTransform.rect.width;
        float ratio = width / rowTransform.rect.width;
        float height = rowTransform.rect.height * ratio;

        float scrollHeight = height * itemCount;
        invitesGridTransform.offsetMin = new Vector2(invitesGridTransform.offsetMin.x, -scrollHeight / 2);
        invitesGridTransform.offsetMax = new Vector2(invitesGridTransform.offsetMax.x, scrollHeight / 2);

        for (int i = 0; i < itemCount; i++)
        {

            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = playersInParty[i] + " member tile";
            newItem.transform.parent = membersGrid.transform;
            newItem.SetActive(true);

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

    public void startInvitationPanel()
    {
        invitationsPanel.SetActive(true);
        guildPanel.SetActive(false);

        getInvitationsListFromServer();

        invitesPending.Sort();

        foreach (Transform g in invitesGrid.transform)
            GameObject.Destroy(g.gameObject);

        RectTransform rowTransform = invitationTile.GetComponent<RectTransform>();
        RectTransform invitesGridTransform = invitesGrid.GetComponent<RectTransform>();

        float width = invitesGridTransform.rect.width;
        float ratio = width / rowTransform.rect.width;
        float height = rowTransform.rect.height * ratio;

        float scrollHeight = height * itemCount;
        invitesGridTransform.offsetMin = new Vector2(invitesGridTransform.offsetMin.x, -scrollHeight / 2);
        invitesGridTransform.offsetMax = new Vector2(invitesGridTransform.offsetMax.x, scrollHeight / 2);

        for (int i = 0; i < itemCount; i++)
        {
            GameObject newItem = Instantiate(invitationTile) as GameObject;
            newItem.name = invitesPending[i] + " invite tile";
            newItem.transform.parent = invitesGrid.transform;
            newItem.SetActive(true);

            Text newItemsText = newItem.GetComponentInChildren<Text>();
            newItemsText.text = invitesPending[i];

            Button newItemsButton = newItem.transform.Find("AcceptButton").GetComponent<Button>();
            newItemsButton.onClick.AddListener(() => acceptThisInvite(newItemsText));

            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -invitesGridTransform.rect.width / 2 + width;
            float y = invitesGridTransform.rect.height / 2 - height;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);
        }
    }

    public void refresh()
    {
        if (wasAlreadyStarted)
        {
            if (PlayerDataManager.Instance.currentPlayerData.guild == "")
            {
                guildPanel.SetActive(false);
                invitationsPanel.SetActive(true);
                updateInvitesList();
            }
            else
            {
                guildPanel.SetActive(true);
                invitationsPanel.SetActive(false);
                updatePlayersInPartyList();
            }
        }
        else
        {

            wasAlreadyStarted = true;
            Start();
        }
    }

    void Start()
    {
        invitationTile.SetActive(false);
        if (PlayerDataManager.Instance.currentPlayerData.guild == "")
            startInvitationPanel();
        else
            startGuildPanel();
    }
}
