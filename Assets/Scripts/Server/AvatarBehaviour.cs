﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarBehaviour : MonoBehaviour {

  private bool _alive = true;

  public float MoveSpeed = 100.0f;

  private float _playerDrivenMovement = float.NaN;
  private float _playerDrivenIntensity = 0.0f;

  public bool IsAlive { get { return _alive; } }
  public float PlayerForceAngle { get { return _playerDrivenMovement; } }

  public bool HasNetworkController = false;

  private void Update()
  {
    AddPlayerDrivenMovementForce();
  }

  // Update is called once per frame
  public void SetPlayerDrivenMovement(float angleOfForce, float intensity)
  {
    if (_alive)
    {
      _playerDrivenMovement = angleOfForce;
      _playerDrivenIntensity = intensity;
    }
  }

  private void AddPlayerDrivenMovementForce()
  {
    //Precondition is that our prime variable is a number and valid angle
    if(float.IsNaN(_playerDrivenMovement) || _playerDrivenMovement < 0.0f || _playerDrivenMovement > 360.0f)
    {
      return;
    }

    //If preconditions met, make some calculations!
    float radAngle = Mathf.Deg2Rad * _playerDrivenMovement;
    float forceMagnitude = MoveSpeed * Time.deltaTime * _playerDrivenIntensity;

    //Determine the angle with simple trig
    var forceVector = new Vector2(Mathf.Cos(radAngle) * forceMagnitude, Mathf.Sin(radAngle) * forceMagnitude);

    //Apply the force to the game object
    gameObject.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Force);
  }

  public void Kill()
  {
    _alive = false;
    _playerDrivenMovement = float.NaN;
    gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
  }
}
