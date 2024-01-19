using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using TMPro;

public class Relay : MonoBehaviour
{
    public TextMeshProUGUI joinCodeText;
    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();


        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);


            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();


            Debug.Log("jc: "+joinCode);
            joinCodeText.text = "Your Lobby Code: " + joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinRelay(TMP_InputField tmpuf)
    {
        try
        {
            Debug.Log("Joining " + tmpuf.text);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(tmpuf.text);
            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();
            Debug.Log("Joined");
            //SceneManager.LoadScene("Online Game");
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
