using System;
using HarmonyLib;
using StardewModdingAPI;
using StardewValley;

namespace ARVConversationTopics
{
    // Applies Harmony patches to Event.cs to add a conversation topic for luau results.
    public class LuauPatcher
    {
        private static IMonitor Monitor;
        private static ModConfig Config;

        // call this method from your Entry class
        public static void Initialize(IMonitor monitor, ModConfig config)
        {
            Monitor = monitor;
            Config = config;
        }

        // Method to apply harmony patch
        public static void Apply(Harmony harmony)
        {
            try
            {
                harmony.Patch(
                    original: AccessTools.Method(typeof(Event), "governorTaste"),
                    postfix: new HarmonyMethod(typeof(LuauPatcher), nameof(LuauPatcher.Event_governorTaste_Postfix))
                );
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to add postfix ARV luau patch with exception: {ex}", LogLevel.Error);
            }
        }

        // Method that is used to postfix
        private static void Event_governorTaste_Postfix(Event __instance)
        {
            try
            {
                foreach (Item luauIngredient in Game1.player.team.luauIngredients)
                {
                    if (luauIngredient.ParentSheetIndex == 282)
                    {
                        Game1.player.activeDialogueEvents.Add("ARV_luau_cranberries", Config.LuauDuration);
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                Monitor.Log($"Failed to add ARV luau conversation topic with exception: {ex}", LogLevel.Error);
            }
        }
    }

}
