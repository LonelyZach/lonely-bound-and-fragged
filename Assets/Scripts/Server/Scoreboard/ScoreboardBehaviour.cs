using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardBehaviour : MonoBehaviour
{
  public GameObject DefaultScoreboardPlayerInfo;
  public RectTransform playerListContentTransform;

  protected VerticalLayoutGroup _layout;
  protected List<NetworkPlayerBehaviour> _players = new List<NetworkPlayerBehaviour>();
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

  public void AddPlayer(NetworkPlayerBehaviour player)
  {
    if (_players.Contains(player))
      return;

    _players.Add(player);

    var scoreboardPlayer = Instantiate(DefaultScoreboardPlayerInfo);
    var scoreboardPlayerBehaviour = scoreboardPlayer.GetComponent<ScoreboardPlayerInfo>();

    scoreboardPlayerBehaviour.playerData = player.playerData;
    _playerInfos.Add(scoreboardPlayerBehaviour);

    scoreboardPlayerBehaviour.transform.SetParent(playerListContentTransform, false);
    PlayerListModified();
  }

  public void PlayerListModified()
  {
  }
}
