﻿using AMDaemon;
using AMDaemon.Allnet;
using HarmonyLib;
using Manager;
using Manager.Operation;
using IniFile = MAI2System.IniFile;
using Network = AMDaemon.Network;

namespace AquaMai.Fix;

public class BasicFix
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(IniFile), "clear")]
    private static bool PreIniFileClear()
    {
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(LanInstall), "IsServer", MethodType.Getter)]
    private static bool PreIsServer(ref bool __result)
    {
        __result = true;
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Network), "IsLanAvailable", MethodType.Getter)]
    private static bool PreIsLanAvailable(ref bool __result)
    {
        __result = false;
        return false;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(OperationManager), "CheckAuth_Proc")]
    private static void PostCheckAuthProc(ref OperationData ____operationData)
    {
        if (Auth.GameServerUri.StartsWith("http://") || Auth.GameServerUri.StartsWith("https://"))
        {
            ____operationData.ServerUri = Auth.GameServerUri;
        }
    }
}
