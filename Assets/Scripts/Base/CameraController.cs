using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothing = 0.125f;

    private int numberOfPlayers;
    private Vector3 defaultZoom;
    public List<GameObject> activePlayers { get; private set; }

    private void Awake()
    {
        activePlayers = new List<GameObject>();
        defaultZoom = transform.position;
    }

    void LateUpdate()
    {
        numberOfPlayers = activePlayers.Count;
        if (numberOfPlayers > 0)
        {
            MoveCamera();
            ZoomCamera();
        }
    }

    private void MoveCamera()
    {
        Vector3 centre = GetCentrePoint();
        Vector3 newPostion = centre + offset;

        transform.position = Vector3.Lerp(transform.position, newPostion, smoothing * Time.deltaTime);
    }

    private void ZoomCamera()
    {
        Vector3 distance = GetDistance();
        transform.position = Vector3.Lerp(transform.position, distance, smoothing * Time.deltaTime);
    }

    private Vector3 GetCentrePoint()
    {
        Vector3 value;

        if (numberOfPlayers > 0)
        {
            Vector4 values = new Vector4();

            for (int i = 0; i < activePlayers.Count; i++)
            {
                values.x += activePlayers[i].transform.position.x;
                values.y += activePlayers[i].transform.position.z;
            }

            values.z = values.x / numberOfPlayers;
            values.w = values.y / numberOfPlayers;

            value = new Vector3(values.z, 0, values.w);

            return value;
        }
        else if (numberOfPlayers == 1)
            return activePlayers[0].transform.position;
        else
            return Vector3.zero;
    }

    private Vector3 GetDistance()
    {
        if (numberOfPlayers > 1)
        {
            float distance = 0;
            float temp;
            for (int i = 0; i < activePlayers.Count - 1; i++)
            {
                temp = Vector3.Distance(activePlayers[i].transform.position, activePlayers[i + 1].transform.position);

                if (temp > distance)
                    distance = temp;
            }

            temp = Vector3.Distance(activePlayers[0].transform.position, activePlayers[numberOfPlayers - 1].transform.position);
            float zOffset = 0.75f;
            Vector3 zoomPosition = new Vector3
            {
                x = transform.position.x,
                y = transform.position.y + distance,
                z = transform.position.z - distance * zOffset
            };

            return zoomPosition;
        }
        else
            return offset + GetCentrePoint();
    }
}