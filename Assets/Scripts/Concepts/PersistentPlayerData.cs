using UnityEngine;
using UnityEditor;

public struct PersistentPlayerData
{
  public readonly int playerId;
  public Color playerColor;
  public string playerName;
  public int numberOfGames;

  public PersistentPlayerData(Color color, string name)
  {
    System.Random random = new System.Random();
    playerId = random.Next();

    playerColor = color;
    playerName = name;
    numberOfGames = 0;
  }

  public override string ToString()
  {
    return "Id: " + playerId + " Name: " + playerName;
  }
}