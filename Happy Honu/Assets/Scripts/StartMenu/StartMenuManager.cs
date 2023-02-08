using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb; // for interacting with the SDK
using System.Threading.Tasks; // for some async functionality

public class StartMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Create references for the two Game Objects we created
    public GameObject ConnectedState;
    public GameObject DisconnectedState;
    private ThirdwebSDK sdk;
    void Start()
    {
          // Configure the network you deployed your contracts to:
      sdk = new ThirdwebSDK("goerli");  
    }

    public async void ConnectWallet()
    {
      // Connect to the wallet
      string address =
          await sdk
              .wallet
              .Connect(new WalletConnection()
              {
                  provider = WalletProvider.CoinbaseWallet,
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
          ? "Welcome to Happy Honu! Pick a 🐢 to mint:"
          : "You can't access this without a Key!";

      // Set the ConnectedStates "Balance" GameObjects text to the wallet balance
        ConnectedState
          .transform
          .Find("Owns NFT")
          .GetComponent<TMPro.TextMeshProUGUI>()
          .text = balanceText; 
    }

    public async Task<string> CheckBalance()
    {
      // Connect to the smart contract
      // Replace this with your own contract address
      Contract contract = sdk.GetContract("0xd6343467537998b1EDa668466126cb5b772c2EA1"); // my addy
      // Contract contract = sdk.GetContract("0x69BC6d095517951Df17f70f38746Bc27CE1C8A62"); // tutorial's addy
      // Replace this with your NFT's token ID
      string tokenId = "0";

      // Check the balance of the wallet for this NFT
      string balance = await contract.ERC1155.Balance(tokenId);
      return balance;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
