using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WallCollider : MonoBehaviour
{
    public Transform BL;
    public Transform TL;
    public Transform BR;
    public Transform TR;
    private float startPos;

    LineRenderer lineRenderer; // would use this to draw border
    public float edgeWidth=0.02f;
    void Awake()
    {
        Application.targetFrameRate = 60;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5;
        lineRenderer.startWidth = edgeWidth;
        AddCollider();
    }

    void AddCollider()
    {
        if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

        var cam = Camera.main;
        if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

        startPos = BL.position.x;
        var scale = (BL.position.x / startPos);

        var bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        BL.position = bottomLeft;
        BL.position.Scale(new Vector3(1, 1, 0));
        BL.localScale *= scale;
        var topLeft = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        TL.position = topLeft;
        TL.position.Scale(new Vector3(1, 1, 0));
        TL.localScale *= scale;
        var topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        TR.position = topRight;
        TR.position.Scale(new Vector3(1, 1, 0));
        TR.localScale *= scale;
        var bottomRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
        BR.position = bottomRight;
        BR.position.Scale(new Vector3(1, 1, 0));
        BR.localScale *= scale;

        // add or use existing EdgeCollider2D
        var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();
        //edge.sharedMaterial.bounciness = 1;
        var edgePoints2d = new[] { (Vector2)bottomLeft, (Vector2)topLeft, (Vector2)topRight, (Vector2)bottomRight, (Vector2)bottomLeft };
        edge.points = edgePoints2d;
        
        // this code would draw the border, i dont think we need it
        var edgePoints3d = new[] { bottomLeft, topLeft, topRight, bottomRight, bottomLeft };
        lineRenderer.SetPositions(edgePoints3d);
    }


}