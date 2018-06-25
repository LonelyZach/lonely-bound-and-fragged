using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody2D), typeof(EdgeCollider2D), typeof(LineRenderer))]
public class EllipseEdgeCollider2D : MonoBehaviour
{
  public float XRadius = 1.0f;
  public float YRadius = 1.0f;
  public int NumPoints = 32;

  EdgeCollider2D EdgeCollider;
  LineRenderer lineRenderer;
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
    if (NumPoints != EdgeCollider.pointCount || NumPoints != lineRenderer.positionCount || 
          XRadius != currentXRadius || YRadius != currentYRadius)
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
    Vector3[] drawPoints = new Vector3[NumPoints + 1];
    EdgeCollider = GetComponent<EdgeCollider2D>();
    lineRenderer = GetComponent<LineRenderer>();

    var transform = GetComponent<Transform>();

    for (int loop = 0; loop <= NumPoints; loop++)
    {
      float angle = (Mathf.PI * 2.0f / NumPoints) * loop;
      var newVector = new Vector2(XRadius * Mathf.Cos(angle), YRadius * Mathf.Sin(angle));
      edgePoints[loop] = newVector;
      drawPoints[loop] = new Vector3(newVector.x + transform.position.x, newVector.y + transform.position.y);
    }

    EdgeCollider.points = edgePoints;
    lineRenderer.positionCount = NumPoints;
    lineRenderer.SetPositions(drawPoints);
    lineRenderer.material.color = Color.white;
    currentXRadius = XRadius;
    currentYRadius = YRadius;
  }
}