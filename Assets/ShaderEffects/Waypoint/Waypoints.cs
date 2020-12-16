using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Texture2D waypointLineTexture;
    [SerializeField]
    private Shader m_waypointLineShader;
    private Material m_waypointLineMaterial;

    //list of waypoints - from the children
    [SerializeField]
    private List<WaypointObject> m_waypoints;

    private LineRenderer m_waypointLine;
    [SerializeField]
    private float m_lineRendererWidth = 0.5f;

    private void OnEnable()
    {
        if(!m_waypointLineShader || !waypointLineTexture)
        {
            enabled = false;
            return;
        }
        m_waypointLineMaterial = new Material(m_waypointLineShader);
        m_waypointLineMaterial.mainTexture = waypointLineTexture;
        m_waypointLineMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_waypoints = transform.GetComponentsInChildren<WaypointObject>().ToList();
        if (m_waypoints.Count <= 0)
        {
            return;
        }
        GenerateLineRenderer();
        GenerateLinePoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateLineRenderer()
    {
        m_waypointLine = gameObject.AddComponent<LineRenderer>();
        m_waypointLine.material = m_waypointLineMaterial;
        m_waypointLine.startWidth = m_lineRendererWidth;
        m_waypointLine.endWidth = m_lineRendererWidth;
        m_waypointLine.textureMode = LineTextureMode.RepeatPerSegment;
    }

    private void GenerateLinePoints()
    {
        m_waypointLine.positionCount = m_waypoints.Count;
        List<Vector3> waypointPositions = new List<Vector3>();
        foreach (var waypoint in m_waypoints)
        {
            waypointPositions.Add(waypoint.transform.position);
        }
        m_waypointLine.SetPositions(waypointPositions.ToArray());
    }
}
