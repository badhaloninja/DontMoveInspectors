using FrooxEngine;
using HarmonyLib;
using NeosModLoader;

namespace DontMoveInspectors
{
    public class DontMoveInspectors : NeosMod
    {
        public override string Name => "DontMoveInspectors";
        public override string Author => "badhaloninja";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/badhaloninja/DontMoveInspectors";

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<bool> Enabled = new ModConfigurationKey<bool>("enabled", "Stop inspectors from moving back", ()=>true);

        private static ModConfiguration config;
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.badhaloninja.DontMoveInspectors");
            harmony.PatchAll();

            config = GetConfiguration();
        }



        [HarmonyPatch(typeof(InspectorHelper), "OpenInspectorForTarget")]
        private class InspectorHelper_OpenInspectorForTarget_Patch
        {
            public static void Prefix(ref Slot source)
            {
                if (!config.GetValue(Enabled)) return;
                source = null;
            }
        }
    }
}