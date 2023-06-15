using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine.SceneManagement;
using UnityEngine;


public class NetworkUI : NetworkBehaviour
{
   private NetworkVariable<int> players = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] private Text winText;
    [SerializeField] private GameObject scoreboard;
    [SerializeField] private Text[] playerwintext;
    [SerializeField] ParticleSystem ps;
    bool[] boosted = new bool[4];
    GameObject[] pla;

    private void Start()
    {
        scoreboard.SetActive(false);

    }
    /*  [SerializeField] private Button hostbutton;
      [SerializeField] private Button joinbutton;
      [SerializeField] InputField _joinInput;

      private UnityTransport _transport;
      // Start is called before the first frame update
      async void Start()
      {
          //_transport = FindObjectOfType<UnityTransport>();
          //await authenticate();

      }
      private static async Task authenticate()
      {
          await UnityServices.InitializeAsync();
          await AuthenticationService.Instance.SignInAnonymouslyAsync();
      }
      // Update is called once per frame*/
    void Update()
    {

        pla = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < pla.Length; i++)
        {
            pla[i].GetComponent<glide>().SetPlayerNum(i+1);

        }
        players.Value = pla.Length;
    }
    /*
    public async void host()
    {
        Allocation a = await RelayService.Instance.CreateAllocationAsync(4);
        joincodetext.text = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
        _transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);
        NetworkManager.Singleton.StartHost();
    }
    public async void client()
    {
        JoinAllocation a = await RelayService.Instance.JoinAllocationAsync(_joinInput.text);
        _transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);
        NetworkManager.Singleton.StartClient();
        
    }
   */
    public IEnumerator waitfornextscene()
    {
        yield return new WaitForSeconds(3f);
        if(SceneManager.GetActiveScene().name == "1-1")
        {
            NetworkManager.Singleton.SceneManager.LoadScene("1-2", LoadSceneMode.Single);

        }
    }
    public void scoreVisibility(bool activity)
    {
        scoreboard.SetActive(activity);

    }
    public void win(int playernum)
    {
  
            
        pla = GameObject.FindGameObjectsWithTag("Player");
        winText.text = "Player " + (playernum) + " Wins!";
        playerwintext[playernum - 1].text = "Player " + playernum + ": " + pla[playernum - 1].GetComponent<glide>().getwinsmethod(playernum - 1);
        scoreVisibility(true);
        StartCoroutine(waitfornextscene());



    }
    public void boostparticle(GameObject playa, int color)
    {
        if (color == 3)
        {
            GameObject part = GameObject.Instantiate(ps.gameObject, playa.transform.position, ps.transform.rotation);
            ParticleSystem.MainModule mainmodule = part.GetComponent<ParticleSystem>().main;
            mainmodule.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.black);
            part.GetComponent<ParticleSystem>().Play();
            return;

        }
        if (color ==2)
        {
            GameObject part = GameObject.Instantiate(ps.gameObject, playa.transform.position, ps.transform.rotation);
            ParticleSystem.MainModule mainmodule = part.GetComponent<ParticleSystem>().main;
            mainmodule.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.black);
            part.GetComponent<ParticleSystem>().Play();
            return;
        }
        if (color == 1)
        {
            GameObject part = GameObject.Instantiate(ps.gameObject, playa.transform.position, ps.transform.rotation);
            part.GetComponent<ParticleSystem>().startColor = Color.blue;
            part.GetComponent<ParticleSystem>().Play();
        }
        if (color == 0)
        {
            GameObject part = GameObject.Instantiate(ps.gameObject, playa.transform.position, ps.transform.rotation);
            part.GetComponent<ParticleSystem>().startColor = Color.red;

            part.GetComponent<ParticleSystem>().Play();
        }

        
    }

                

    
}
