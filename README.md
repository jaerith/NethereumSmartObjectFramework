# NethereumSmartObjectFramework
This project aims to create a .NET library that integrates with both the Smart Object Framework (used within the game Eve Frontier) and the browser wallet Eve Vault.

![Authenicating](https://github.com/jaerith/NethereumSmartObjectFramework/blob/main/Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Authenticating.png)

## Running the Dapp Demo

1. Run two instances of Visual Studio, both opened to the solution of this repo.

2. In one instance of VS, start the REST API Server in Debug mode.

3. In the other instance of VS, start the [WASM Server](Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Launch.png) in Debug mode.

4. Authenticate the current user of the Eve Vault [via the button that triggers SIWE authentication](Screenshots/Eve_Frontier_Ethereum_Dapp_Prototype_Authenticating.png).

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
