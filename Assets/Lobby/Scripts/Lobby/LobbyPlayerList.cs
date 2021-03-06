using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Prototype.NetworkLobby
{
  //List of players in the lobby
  public class LobbyPlayerList : MonoBehaviour
  {
    public static LobbyPlayerList _instance = null;

    public RectTransform playerListContentTransform;
    public GameObject oddPlayerWarning;
    public GameObject minPlayerWarning;
    public Transform addButtonRow;

    protected VerticalLayoutGroup _layout;
    protected List<LobbyPlayer> _players = new List<LobbyPlayer>();

    public void OnEnable()
    {
      _instance = this;
      _layout = playerListContentTransform.GetComponent<VerticalLayoutGroup>();
    }

    public void DisplayOddPlayerWarning(bool enabled)
    {
      if (oddPlayerWarning != null)
        oddPlayerWarning.SetActive(enabled);
    }

    public void DisplayMinPlayerWarning(bool enabled)
    {
      if (minPlayerWarning != null)
        minPlayerWarning.SetActive(enabled);
    }

    void Update()
    {
      //this dirty the layout to force it to recompute evryframe (a sync problem between client/server
      //sometime to child being assigned before layout was enabled/init, leading to broken layouting)

      if (_layout)
        _layout.childAlignment = Time.frameCount % 2 == 0 ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;
    }

    public void AddPlayer(LobbyPlayer player)
    {
      if (_players.Contains(player))
        return;

      _players.Add(player);

      player.transform.SetParent(playerListContentTransform, false);
      addButtonRow.transform.SetAsLastSibling();

      PlayerListModified();
    }

    public void RemovePlayer(LobbyPlayer player, bool isBeingDestroyed = false)
    {
      _players.Remove(player);
      PlayerListModified();

      //This hides some client cleanup errors
      if (!isBeingDestroyed)
      {
        var numPlayers = LobbyManager.s_Singleton.numPlayers;
        LobbyPlayerList._instance.DisplayOddPlayerWarning(numPlayers % 2 == 1 && numPlayers >= 4);
        LobbyPlayerList._instance.DisplayMinPlayerWarning(numPlayers < 4);
      }
    }

    public void PlayerListModified()
    {
      int i = 0;
      foreach (LobbyPlayer p in _players)
      {
        p.OnPlayerListChanged(i);
        ++i;
      }
    }
  }
}
