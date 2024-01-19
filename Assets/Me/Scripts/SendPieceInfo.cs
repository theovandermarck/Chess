using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SendPieceInfo : NetworkBehaviour
{
    public NetworkVariable<Vector2> newPiecePos = new NetworkVariable<Vector2>(new Vector2(8, 8), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Vector2> oldPiecePos = new NetworkVariable<Vector2>(new Vector2(8, 8), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public BoardDrawerOnline boardDrawerOnline;
    public Vector2 oldPos = new Vector2(8, 8);
    public Vector2 newPos = new Vector2(8, 8);
    //public override void OnNetworkSpawn()
    //{
    //    newPiecePos.OnValueChanged += (Vector2 previousValue, Vector2 newValue) =>
    //    {
    //        Debug.LogError("changedvalue"+newValue);
    //        if (oldPiecePos.Value != new Vector2(8, 8) && !IsOwner)
    //        {
    //            Debug.LogError(OwnerClientId + "; " + oldPiecePos.Value + newPiecePos.Value);
    //        }
    //    };
    //}

    void Start()
    {
        
        //piecesPoses.Value = new Vector2(8, 8);
    }
    void Update()
    {
        try
        {
            boardDrawerOnline = GameObject.Find("Canvas").GetComponent<BoardDrawerOnline>();
            if (IsOwner)
            {
                boardDrawerOnline.tellGameObjectForScript(this.gameObject);
            }
        }catch
        {

        }
        if (!IsOwner)
        {
            //if (Input.GetMouseButtonUp(0))
            //{
            //    Debug.LogError("MD; " + OwnerClientId + "; " + oldPiecePos.Value + newPiecePos.Value);
            //}
            if (newPiecePos.Value != newPos || oldPiecePos.Value != oldPos)
            {
                Debug.LogError(OwnerClientId + "; " + oldPiecePos.Value + newPiecePos.Value);
                newPos = newPiecePos.Value;
                oldPos = oldPiecePos.Value;
            }
        }
    }
    public void ChangeValue(Vector2 nOldPos, Vector2 nNewPos)
    {
        oldPiecePos.Value = nOldPos;
        newPiecePos.Value = nNewPos;
        //Debug.LogError(!IsOwner+"; "+OwnerClientId + "; " + oldPiecePos.Value + newPiecePos.Value);
    }

    public bool CheckIfServer()
    {
        return IsServer;
    }
}
