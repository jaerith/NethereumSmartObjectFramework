// SPDX-License-Identifier: MIT
pragma solidity >=0.8.24;
 
import { Script } from "forge-std/Script.sol";
import { console } from "forge-std/console.sol";
 
import { IWorld } from "../../src/codegen/world/IWorld.sol";
 
import { SystemHook } from "@latticexyz/world/src/SystemHook.sol";
 
import { ResourceId, WorldResourceIdLib, WorldResourceIdInstance } from "@latticexyz/world/src/WorldResourceId.sol";
import { RESOURCE_SYSTEM } from "@latticexyz/world/src/worldResourceTypes.sol";
import { BEFORE_CALL_SYSTEM, AFTER_CALL_SYSTEM } from "@latticexyz/world/src/systemHookTypes.sol";
 
contract InventorySystemHook is SystemHook {

  event InventoryBeforeEvent(address indexed _msgSender, 
                             ResourceId indexed _systemId,
                             bytes callData);

  event InventoryAfterEvent(address indexed _msgSender, 
                            ResourceId indexed _systemId,
                            bytes callData);

  function onBeforeCallSystem(address msgSender, ResourceId systemId, bytes memory callData) external {

    emit InventoryBeforeEvent(msgSender, systemId, callData);

    return;
  }
 
  function onAfterCallSystem(address msgSender, ResourceId systemId, bytes memory callData) external {

    emit InventoryAfterEvent(msgSender, systemId, callData);

    return;
  }
}