using UnityEngine;
using System.Collections;

public class KeyboardController : PlayerBehaviour
{
  public KeyCode Up;
  public KeyCode Down;
  public KeyCode Left;
  public KeyCode Right;


  // Use this for initialization
  void Start()
  {
    FindAssociatedAvatarBehaviour();
  }

  // Update is called once per frame
  void Update()
  {
    var isUp = Input.GetKey(Up);
    var isDown = Input.GetKey(Down);
    var isLeft = Input.GetKey(Left);
    var isRight = Input.GetKey(Right);

    //Angle in degrees
    float angleOfForce = float.NaN;

    //First cancel out opposing key presses
    if(isUp && isDown)
    {
      isUp = false;
      isDown = false;
    }
    if(isLeft && isRight)
    {
      isLeft = false;
      isRight = false;
    }

    if (isUp)
    {
      if (isLeft)
      {
        angleOfForce = 315.0f;
      }
      if (isRight)
      {
        angleOfForce = 45.0f;
      }
      else
      {
        angleOfForce = 0.0f;
      }
    }
    else if (isDown)
    {
      if (isLeft)
      {
        angleOfForce = 215.0f;
      }
      if (isRight)
      {
        angleOfForce = 135.0f;
      }
      else
      {
        angleOfForce = 180.0f;
      }
    }
    else if(isLeft)
    {
      angleOfForce = 270.0f;
    }
    else if(isRight)
    {
      angleOfForce = 90.0f;
    }

    //This is done whether or not we achieve a real angle, 
    //because we need to know if one is being applied at all
    Cmd_SetPlayerDrivenMovement(angleOfForce);
  }

  public static KeyboardController KeyboardWASDController()
  {
    KeyboardController kc = new KeyboardController();
    kc.Up = KeyCode.W;
    kc.Down = KeyCode.S;
    kc.Left = KeyCode.A;
    kc.Right = KeyCode.D;
    return kc;
  }

  public static KeyboardController KeyboardArrowKeyController()
  {
    KeyboardController kc = new KeyboardController();
    kc.Up = KeyCode.UpArrow;
    kc.Down = KeyCode.DownArrow;
    kc.Left = KeyCode.LeftArrow;
    kc.Right = KeyCode.RightArrow;
    return kc;
  }
}
