using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreboardPlayerInfo : NetworkBehaviour
{
  public Text playerName;
  public Text gamesPlayed;
  public Text killsScored;
  public Text wins;
  public Text rank;

  [SyncVar]
  public PersistentPlayerData playerData;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    playerName.text = playerData.playerName;
    gamesPlayed.text = playerData.numberOfGames.ToString();
    killsScored.text = playerData.kills.ToString();
    wins.text = playerData.wins.ToString();
    rank.text = playerData.rank == 0 ? "" : playerData.rank.ToString();
  }

  [ClientRpc]
  public void RpcSetParent(GameObject childScoreboardPlayer, GameObject parentScoreboard)
  {
    childScoreboardPlayer.transform.SetParent(parentScoreboard.transform);
  }
}
