using System;
using Microsoft.Xna.Framework;
using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace ARVConversationTopics
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {

        // Properties
        private ModConfig Config;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // Read in config file and create if needed
            this.Config = this.Helper.ReadConfig<ModConfig>();

            // Initialize the error logger in WeddingPatcher
            LuauPatcher.Initialize(this.Monitor, this.Config);
            RepeatPatcher.Initialize(this.Monitor, this.Config);

            // Do the Harmony things
            var harmony = new Harmony(this.ModManifest.UniqueID);
            LuauPatcher.Apply(harmony);
            RepeatPatcher.Apply(harmony);            
        }

        // Helper function to check if a string is on the list of repeatable CTs added by this mod
        public static Boolean isRepeatableCTAddedByMod(string topic)
        {
            string[] modRepeatableConversationTopics = new string[] { "ARV_luau_cranberries" };
            foreach (string s in modRepeatableConversationTopics)
            {
                if (s == topic)
                {
                    return true;
                }
            }
            return false;
        }
    }
}