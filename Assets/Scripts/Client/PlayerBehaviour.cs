using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour {

  public bool MoveWithWasd = false;
  public bool MoveWitArrowKeys = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    var directions = new List<Direction>();

    if ((MoveWithWasd && Input.GetKey(KeyCode.W)) || (MoveWitArrowKeys && Input.GetKey(KeyCode.UpArrow)))
    {
      directions.Add(Direction.Up);
    }
    if ((MoveWithWasd && Input.GetKey(KeyCode.A)) || (MoveWitArrowKeys && Input.GetKey(KeyCode.LeftArrow)))
    {
      directions.Add(Direction.Left);
    }
    if ((MoveWithWasd && Input.GetKey(KeyCode.S)) || (MoveWitArrowKeys && Input.GetKey(KeyCode.DownArrow)))
    {
      directions.Add(Direction.Down);
    }
    if ((MoveWithWasd && Input.GetKey(KeyCode.D)) || (MoveWitArrowKeys && Input.GetKey(KeyCode.RightArrow)))
    {
      directions.Add(Direction.Right);
    }

    Cmd_SetPlayerDrivenMovement(directions);
  }

  // [Command] - enable for netwokring
  void Cmd_SetPlayerDrivenMovement(List<Direction> directions)
  {
    gameObject.GetComponent<AvatarBehaviour>().SetPlayerDrivenMovement(directions);
  }
}
