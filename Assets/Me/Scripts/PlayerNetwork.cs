using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour
{
    public GameObject PlayerTransform;
    public float speed;
    public GameObject multiplayerMenu;
    private bool notStarted = true;
    // Start is called before the first frame update
    private NetworkVariable<Vector2> piecesPoses = new NetworkVariable<Vector2>(new Vector2(8, 8), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        piecesPoses.OnValueChanged += (Vector2 previousValue, Vector2 newValue) =>
        {
            if (previousValue != new Vector2(8, 8))
            {
                Debug.Log(OwnerClientId + "; " + previousValue + newValue);
            }
        };
    }
    void Update()
    {
        try
        {
            if (!IsOwner && notStarted && IsHost)
            {
                NetworkManager.SceneManager.LoadScene("Online Game", LoadSceneMode.Single);
                notStarted = false;
            }
        }
        catch
        {

        }
    }
    public void changePiecePoses(Vector2 oldPos, Vector2 newPos)
    {
        piecesPoses.Value = oldPos;
        piecesPoses.Value = newPos;
    }
}
