﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarBehaviour : MonoBehaviour {

  private bool _dead = false;

  public float MoveSpeed = 100.0f;

  private List<Direction> _playerDrivenMovement = new List<Direction>();

  private void Update()
  {
    if (!_dead)
    {
      AddPlaterDrivenMovementForce();
    }
  }

  // Update is called once per frame
  public void SetPlayerDrivenMovement(List<Direction> directions)
  {
    _playerDrivenMovement = directions;
  }

  private void AddPlaterDrivenMovementForce()
  {
    if (_playerDrivenMovement.Contains(Direction.Up))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, MoveSpeed * Time.deltaTime), ForceMode2D.Force);
    }
    if (_playerDrivenMovement.Contains(Direction.Left))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-MoveSpeed * Time.deltaTime, 0), ForceMode2D.Force);
    }
    if (_playerDrivenMovement.Contains(Direction.Down))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -MoveSpeed * Time.deltaTime), ForceMode2D.Force);
    }
    if (_playerDrivenMovement.Contains(Direction.Right))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(MoveSpeed * Time.deltaTime, 0), ForceMode2D.Force);
    }
  }

  public void Kill()
  {
    _dead = true;
    gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
  }

  public IEnumerable<Direction> GetPlayerDirvenMovementWithOpposingDirectionsIncluded()
  {
    return _playerDrivenMovement;
  }

  public IEnumerable<Direction> GetPlayerDirvenMovementWithOpposingDirectionsIgnored()
  {
    if (_playerDrivenMovement.Contains(Direction.Left) && !_playerDrivenMovement.Contains(Direction.Right))
    {
      yield return Direction.Left;
    }
    if (_playerDrivenMovement.Contains(Direction.Right) && !_playerDrivenMovement.Contains(Direction.Left))
    {
      yield return Direction.Right;
    }
    if (_playerDrivenMovement.Contains(Direction.Up) && !_playerDrivenMovement.Contains(Direction.Down))
    {
      yield return Direction.Up;
    }
    if (_playerDrivenMovement.Contains(Direction.Down) && !_playerDrivenMovement.Contains(Direction.Up))
    {
      yield return Direction.Down;
    }
  }
}
