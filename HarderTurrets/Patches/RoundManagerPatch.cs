using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SigurdLib.Util.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HarderTurrets.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {

        [HarmonyPatch("SpawnMapObjects")]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> HazardSpawnPatch(IEnumerable<CodeInstruction> instructions)
        {

            var instructionList = instructions.ToList();

            for (int i = 0; i < instructionList.Count; i++)
            {
                var instruction = instructionList[i];

                if (instruction.opcode == OpCodes.Conv_I4)
                {
                    var constantLdcI4Instruction = new CodeInstruction(OpCodes.Ldc_I4, 3);
                    var addInstruction = new CodeInstruction(OpCodes.Add);
                    var multiplierLdcI4Instruction = new CodeInstruction(OpCodes.Ldc_I4, HarderTurrets.Instance.turretSpawnMultiplier.Value);
                    var mulInstruction = new CodeInstruction(OpCodes.Mul);

                    instructionList.Insert(i + 1, constantLdcI4Instruction);
                    instructionList.Insert(i + 2, addInstruction);
                    instructionList.Insert(i + 3, multiplierLdcI4Instruction);
                    instructionList.Insert(i + 4, mulInstruction);

                    break;
                }
            }


            return instructionList;
        }

    }
}
