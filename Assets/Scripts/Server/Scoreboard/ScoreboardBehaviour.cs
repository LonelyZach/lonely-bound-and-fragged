using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreboardBehaviour : NetworkBehaviour
{
  public GameObject DefaultScoreboardPlayerInfo;
  public RectTransform playerListContentTransform;

  protected VerticalLayoutGroup _layout;
  protected List<ScoreboardPlayerInfo> _playerInfos = new List<ScoreboardPlayerInfo>();

  public void OnEnable()
  {
    _layout = playerListContentTransform.GetComponent<VerticalLayoutGroup>();
  }

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if(_layout)
    {
      _layout.childAlignment = Time.frameCount % 2 == 0 ? TextAnchor.UpperCenter : TextAnchor.UpperLeft;
    }
  }

  /// <summary>
  /// This is the server call, it immediately passes data to an RPC
  /// </summary>
  /// <param name="playerData"></param>
  public void AddPlayer(PersistentPlayerData playerData)
  {
    RpcAddPlayer(playerData);
  }

  public void PlayerListModified()
  {
  }

  [ClientRpc]
  public void RpcAddPlayer(PersistentPlayerData playerData)
  {
    var scoreboardPlayer = Instantiate(DefaultScoreboardPlayerInfo);
    var scoreboardPlayerBehaviour = scoreboardPlayer.GetComponent<ScoreboardPlayerInfo>();

    scoreboardPlayerBehaviour.playerData = playerData;
    _playerInfos.Add(scoreboardPlayerBehaviour);

    scoreboardPlayerBehaviour.transform.SetParent(playerListContentTransform, false);
  }
}
