# NethereumSmartObjectFramework
This project aims to create a .NET library that integrates with both the Smart Object Framework (used within the game Eve Frontier) and the browser wallet Eve Vault.

![Authenicating](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Authenticating.png)

## Dapp Demo with Nethereum, Eve Vault, and Eve Frontier

### Prerequisite 

[x] Install the [Eve Vault browser extension](https://docs.evefrontier.com/EveVault), making sure that the EV wallet is installed before other ones (Metamask, etc).  
*** Unfortunately, the current version of the Nethereum.Metamask library will only target the first Metamask-compatible wallet found, instead of presenting a choice.  However, a soon-to-be-released version of Nethereum will contain an implementation of the [EIP-6963](https://eips.ethereum.org/EIPS/eip-6963) standard, allowing the wallet to be chosen.

[x] Run the [Smart Storage Unit](https://github.com/projectawakening/builder-examples/tree/develop/smart-storage-unit) from the Project Awakenings repo, replacing the "SmartStorageUnitSystem.sol" contract with [this one](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/NethereumSmartObjectFramework/SmartStorageUnitJaerith/Misc/SmartStorageUnitSystem.jaerith.sol).

### Steps to Run the Dapp

1. Run two instances of Visual Studio, both opened to the solution of this repo.

2. In one instance of VS, start the REST API Server in Debug mode.

3. In the other instance of VS, start the [WASM Server](Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Launch.png) in Debug mode.

4. Validate the current user of the Eve Vault via the button that triggers the [SIWE](https://docs.login.xyz/general-information/siwe-overview) protocol, invoking the [authentication](Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Authenticating.png) of the user.
   
5. Upon authentication, the trade section of the Dapp will now become [available](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Authenticated.png).

6. Set the input quantity to a valid value (like 5) and then hit the Trade button.  The Dapp will then ask [to approve the trade transaction](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Approving_Trade.png), and after the trade is approved, a hash for [the accepted transaction](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Trade_Approved.png) will be generated and displayed.

7. The trade will invoke an event within the smart storage contract, and you can observe these events in the Orders page (listed in the left navigation pane).  Upon entering the Orders page, you will see [a message](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Awaiting_Report.png) asking you to wait, since it takes a few seconds for the block processor to retrieve events from the chain.  However, it should eventually [display details](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Received_Report.png) about the order that you executed in your trade transaction.

**NOTE** : In steps 2 and 3, the ports assigned to the servers during Debug sessions could be different from the ports mentioned in the startup code for the servers - in this case, you should change the ports mentioned in the startup code.  

**NOTE** : In step 7, the Orders page is looking for events from the address of the smart storage contract, which is currently hardcoded in the page.  If the smart storage address is different, then it should be updated to the address mentioned in your local chain.

**Disclaimer** : It appears that the Eve Vault wallet is based on the [OneKey](https://developer.onekey.so/) package.  Repeated interaction between this wallet and the Nethereum.Metamask library can result in an [unstable state]( https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_OneKey_Problem.png) of the Dapp.  The only current fix for this issue is to select the "Remove wallet" option from Eve Vault (i.e., removing the EOA) and then use the "Add Wallet" option, importing the same EOA again with the recovery phrase.  This fix should only be needed temporarily, though, since the next version of the Nethereum.Metamask library will also contain a fix for this issue.

## Dapp Projects

Project Source | Description
------------- | ------------
[REST API Server](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/ExampleProjectSiwe.RestApi) | When run as a separate standalone, this server will receive a signed SIWE message and return the JWT upon validation.
[WASM Server](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/ExampleProjectSiwe.Wasm) | When run as a separate standalone, this Blazor app will run in the browser with the functionality to sign SIWE messages with a Metamask-compatible wallet extension (like Eve Vault) and then to send those signed messages to the REST API (in order to obtain a valid JWT as proof of identity)
[ERC20.Blazor](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/Nethereum.Erc20.Blazor) | This library provides Razor components with Nethereum functionality to be embedded inside the pages of the WASM Server, including both ERC20 and MUD interopability (i.e., the Wrapper Projects mentioned below)

## Wrapper Projects

These libraries wrap around the [builder examples](https://github.com/projectawakening/builder-examples) provided by Eve Frontier.  They showcase the generated classes that are produced when using the Solidity plugin in Visual Code, with the help of the ".nethereum-gen.multisettings" file (placed inside the Solidity project directory).

Project Source | Description
------------- | ------------
[Smart Gate](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartGate) | Wrapper for Smart Gate contracts
[Smart Storage Unit](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartStorageUnit) | Wrapper for Smart Storage Unit contracts
[Smart Storage Unit (Jaerith)](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartStorageUnitJaerith) | Wrapper for a customized version of the Smart Storage Unit contracts, where events are now emitted from the contracts
[Smart Turret](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartTurret) | Wrapper for Smart Turret contracts

## Unit Test Projects

These projects demonstrate how to use the libraries (i.e., wrapper projects).

Project Source | Description
------------- | ------------
[Smart Gate Tests](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartGate.UnitTests) | Tests for Smart Gate wrapper
[Smart Storage Unit Tests](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartStorageUnit.UnitTests) | Tests for Smart Storage Unit wrapper
[Smart Storage Unit (Jaerith) Tests](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartStorageUnitJaerith.UnitTests) | Tests for a customized version of the Smart Storage Unit wrapper
[Smart Turret Tests](https://github.com/jaerith/NethereumSmartObjectFramework/tree/main/NethereumSmartObjectFramework/SmartTurret.UnitTests) | Tests for Smart Turret wrapper
