using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody2D), typeof(EdgeCollider2D))]
public class EllipseEdgeCollider2D : MonoBehaviour
{
  public float XRadius = 1.0f;
  public float YRadius = 1.0f;
  public int NumPoints = 32;

  EdgeCollider2D EdgeCollider;
  float currentXRadius = 0.0f;
  float currentYRadius = 0.0f;

  /// <summary>
  /// Start this instance.
  /// </summary>
  void Start()
  {
    CreateEllipse();
  }

  /// <summary>
  /// Update this instance.
  /// </summary>
  void Update()
  {
    // If the radius or point count has changed, update the circle
    if (NumPoints != EdgeCollider.pointCount || XRadius != currentXRadius || YRadius != currentYRadius)
    {
      CreateEllipse();
    }
  }

  /// <summary>
  /// Creates the circle.
  /// </summary>
  void CreateEllipse()
  {
    Vector2[] edgePoints = new Vector2[NumPoints + 1];
    EdgeCollider = GetComponent<EdgeCollider2D>();

    for (int loop = 0; loop <= NumPoints; loop++)
    {
      float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
      edgePoints[loop] = new Vector2(XRadius * Mathf.Cos(angle), YRadius * Mathf.Sin(angle));
    }

    EdgeCollider.points = edgePoints;
    currentXRadius = XRadius;
    currentYRadius = YRadius;
  }
}