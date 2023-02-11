using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for the toggle
using Thirdweb; // for interacting with the SDK
using System.Threading.Tasks; // for some async functionality

// this is a combination of Thirdweb tutorial and Watase's Dragon Unity Game example.
public class StartMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Create references for the two Game Objects we created
    public GameObject ConnectedState;
    public GameObject DisconnectedState;
    public GameObject ConnectWalletState;
    public GameObject SelectWallet;
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
        // Update is called once per frame
    void Update()
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
    // public void SetSelectWallet()
    // {
    //     ConnectWalletState.SetActive(false);
    //     SelectWallet.SetActive(true);
    // }
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
      
        
        string balance = await CheckBalance();

      // Convert the balance to a float
        float balanceFloat = float.Parse(balance);

      // Create text for the balance depending on if the balance is greater than 0
        string balanceText =
        balanceFloat > 0
          ? "Pick a 🐢 to play:"
          : "You can't access this without a honu 🐢!";

      // Set the ConnectedStates "Balance" GameObjects text to the wallet balance
        // ConnectedState
        //   .transform
        //   .Find("HasNFTState")
        //   .GetComponent<TMPro.TextMeshProUGUI>()
        //   .text = balanceText; 
    }
    public void DisconnectWallet()
    {
      // Disable disconnected state
      DisconnectedState.SetActive(true);

      // Enable connected state
      ConnectedState.SetActive(false);
      
    }

    public async Task<string> CheckBalance()
    {
      // Connect to the smart contract
      // Replace this with your own contract address
      Contract contract = sdk.GetContract("0x69BC6d095517951Df17f70f38746Bc27CE1C8A62"); 
         // Replace this with your NFT's token ID
      string tokenId = "0";

      // Check the balance of the wallet for this NFT
      string balance = await contract.ERC1155.Balance(tokenId);
      return balance;
    }

    //from Watase's Dragon Game
    // public async void ClaimHonu()
    // {
    //     string address = 
    //         await sdk
    //         .wallet
    //         .Connect(new WalletConnection() {
    //             provider = WalletProvider.MetaMask,
    //             chainId = 80001
    //         });
    //     Contract contract = sdk.GetContract("0x1a24bD29aC136BC20191F0B79C4Ad4BbeAea6f66");
    //     await contract.ERC1155.ClaimTo(address, "0", 1);
    // }
    

}
