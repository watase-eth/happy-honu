using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for the toggle
using Thirdweb; // for interacting with the SDK
using System.Threading.Tasks; // for some async functionality
using UnityEngine.SceneManagement;


public class StartMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Create references for the two Game Objects we created
    public GameObject ConnectedState;
    public GameObject DisconnectedState;
    // public GameObject ConnectWalletState;
    // public GameObject SelectWallet;
    public GameObject HasNFTState;
    public GameObject NoNFTState;
    public Toggle HonuToggle;
    public GameObject StartGameButton;

    private ThirdwebSDK sdk;
    void Start()
    {
      // Configure the network you deployed your contracts to:
      sdk = new ThirdwebSDK("goerli");  
      DisconnectedState.SetActive(true);
      ConnectedState.SetActive(false);
      HasNFTState.SetActive(false);
      NoNFTState.SetActive(false);
      HonuToggle.isOn = false;

    }

    private void Update() {
      PlayGame();
    }
    public void PlayGame()
    {
      if(HonuToggle.isOn)
        {
            StartGameButton.SetActive(true);
        }
        else
        {
            StartGameButton.SetActive(false);
        }  
    }

    public async void ConnectWallet()
    {
      // Connect to the wallet
      string address =
          await sdk
              .wallet
              .Connect(new WalletConnection()
              {
                  provider = WalletProvider.MetaMask,
                  chainId = 5 // Switch the wallet Goerli on connection
              });
              
      // Disable disconnected state
      DisconnectedState.SetActive(false);

      // Enable connected state
      ConnectedState.SetActive(true);

      // Set the ConnectedStates "Address" GameObjects text to the wallet address
      ConnectedState
        .transform
        .Find("Address")
        .GetComponent<TMPro.TextMeshProUGUI>()
        .text = address;
      
        
        string balance = await CheckBalance(address);

      // Convert the balance to a float
        float balanceFloat = float.Parse(balance);
        print(balanceFloat);
        if (balanceFloat > 0) {
            HasNFTState.SetActive(true);
            print(true);
          }
        else {
            NoNFTState.SetActive(true);
            print(false);
          }
    }
    public void DisconnectWallet()
    {
      // Disable disconnected state
      DisconnectedState.SetActive(true);

      // Enable connected state
      ConnectedState.SetActive(false);
      
    }

    public async Task<string> CheckBalance(string address)
    {
        Contract contract = sdk.GetContract("0x5e8D21f0bCfD8C57c7dAcE57a2bbd53ad69aa37f");

        string tokenId = "0";

        string balance = await contract.Read<string>("balanceOf", address, tokenId);
        print(balance);
        return balance;
    }

    public async void ClaimHonu()
    {
        string address = 
            await sdk
            .wallet
            .Connect(new WalletConnection() {
                provider = WalletProvider.MetaMask,
                chainId = 5
            });
        Contract contract = sdk.GetContract("0x5e8D21f0bCfD8C57c7dAcE57a2bbd53ad69aa37f");
        await contract.ERC1155.ClaimTo(address, "0", 1);
    }
    

}
