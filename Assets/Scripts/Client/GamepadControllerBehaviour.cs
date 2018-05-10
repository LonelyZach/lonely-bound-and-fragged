using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadControllerBehaviour : PlayerBehaviourBase
{
  public string HorizontalJoystickAxisName;
  public string VerticalJoystickAxisName;

  // Use this for initialization
  void Start ()
  {
    FindAssociatedAvatarBehaviour();
  }
	
	// Update is called once per frame
	void Update ()
  {
    float x = Input.GetAxis(HorizontalJoystickAxisName);
    float y = Input.GetAxis(VerticalJoystickAxisName);


    var unit = new Vector2(x, y);

    if(unit.magnitude > 1.0f)
    {
      unit.Normalize();
    }

    if (Mathf.Abs(x) <= 0.01f && Mathf.Abs(y) <= 0.01f)
    {
      Cmd_SetPlayerDrivenMovement(float.NaN, 0.0f);
    }
    else
    {
      var degrees = Mathf.Rad2Deg * Mathf.Atan2(unit.y, unit.x);
      if (degrees < 0)
      {
        degrees += 360.0f;
      }

      if (unit.magnitude >= 0.01f)
      {
        Cmd_SetPlayerDrivenMovement(degrees, unit.magnitude);
      }
    }
  }
}
