using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour2 : MonoBehaviour {

  public float MoveSpeed = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKey(KeyCode.UpArrow))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, MoveSpeed * Time.deltaTime), ForceMode2D.Force);
    }
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-MoveSpeed * Time.deltaTime, 0), ForceMode2D.Force);
    }
    if (Input.GetKey(KeyCode.DownArrow))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -MoveSpeed * Time.deltaTime), ForceMode2D.Force);
    }
    if (Input.GetKey(KeyCode.RightArrow))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(MoveSpeed * Time.deltaTime, 0), ForceMode2D.Force);
    }
  }
}
