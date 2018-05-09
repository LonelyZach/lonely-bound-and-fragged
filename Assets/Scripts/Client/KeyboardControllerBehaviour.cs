using UnityEngine;
using System.Collections;

public class KeyboardControllerBehaviour : PlayerBehaviourBase
{
  public KeyCode Up;
  public KeyCode Down;
  public KeyCode Left;
  public KeyCode Right;


  // Use this for initialization
  private void Start()
  {
    FindAssociatedAvatarBehaviour();
  }

  // Update is called once per frame
  private void Update()
  {
    var isUp = Input.GetKey(Up);
    var isDown = Input.GetKey(Down);
    var isLeft = Input.GetKey(Left);
    var isRight = Input.GetKey(Right);

    //Angle in degrees
    float angleOfForce = float.NaN;

    //First cancel out opposing key presses
    if (isUp && isDown)
    {
      isUp = false;
      isDown = false;
    }
    if (isLeft && isRight)
    {
      isLeft = false;
      isRight = false;
    }

    if (isRight)
    {
      if (isDown)
      {
        angleOfForce = 315.0f;
      }
      else if (isUp)
      {
        angleOfForce = 45.0f;
      }
      else
      {
        angleOfForce = 0.0f;
      }
    }
    else if (isLeft)
    {
      if (isDown)
      {
        angleOfForce = 215.0f;
      }
      else if (isUp)
      {
        angleOfForce = 135.0f;
      }
      else
      {
        angleOfForce = 180.0f;
      }
    }
    else if (isDown)
    {
      angleOfForce = 270.0f;
    }
    else if (isUp)
    {
      angleOfForce = 90.0f;
    }

    //This is done whether or not we achieve a real angle, 
    //because we need to know if one is being applied at all
    Cmd_SetPlayerDrivenMovement(angleOfForce);
  }
}
