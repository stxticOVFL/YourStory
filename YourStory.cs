using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using TMPro;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace YourStory
{
    [HarmonyPatch]
    public class YourStory : MelonMod
    {
        static GameObject asset;
        static Transform newBackstory;
        public override void OnLateInitializeMelon()
        {
            AssetBundle bundle = AssetBundle.LoadFromMemory(Resources.Resources.AssetBundle);
            asset = bundle.LoadAsset<GameObject>("Assets/YourStory.prefab");
            Settings.Register();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MainMenu), "SetState")]
        public static void SetState(ref MainMenu.State newState)
        {
            if (Settings.Enabled.Value && newState == MainMenu.State.Staging)
            {
                var ogBackstory = RM.ui.portraitUI.transform.parent.Find("Backstory");
                ogBackstory.GetChild(0).gameObject.SetActive(false);
                newBackstory = Utils.InstantiateUI(asset, "YourStory", ogBackstory).transform;
                newBackstory.position = ogBackstory.GetChild(0).transform.position;
                newBackstory.localScale = ogBackstory.GetChild(0).transform.localScale;
            }
            else
                newBackstory = null;
        }

        public override void OnUpdate()
        {
            if (newBackstory && Settings.Enabled.Value)
            {
                var scaler = newBackstory.GetChild(0);
                scaler.GetChild(0).GetComponent<TextMeshPro>().text = Settings.Year.Value;
                scaler.GetChild(1).GetComponent<TextMeshPro>().text = Settings.NeonName.Value;
                scaler.GetChild(2).GetComponent<TextMeshPro>().text = Settings.Title.Value;
                scaler.GetChild(3).GetComponent<TextMeshPro>().text = Settings.Description.Value;
                scaler.GetChild(4).GetComponent<TextMeshPro>().text = Settings.Deserves.Value;
                scaler.GetChild(5).GetComponent<TextMeshPro>().text = Settings.Job.Value;
                scaler.GetChild(6).GetComponent<TextMeshPro>().text = Settings.Sins.Value;

                scaler.GetChild(0).GetComponent<TextMeshPro>().color = Settings.UsedColor.Value;
                scaler.GetChild(1).GetComponent<TextMeshPro>().color = Settings.UsedColor.Value;
                scaler.GetChild(2).GetComponent<TextMeshPro>().color = Settings.UsedColor.Value;
                scaler.GetChild(3).GetComponent<TextMeshPro>().color = Settings.UsedColor.Value;
                scaler.GetChild(4).GetComponent<TextMeshPro>().color = Settings.UsedColor.Value;
                scaler.GetChild(5).GetComponent<TextMeshPro>().color = Settings.UsedColor.Value;
                scaler.GetChild(6).GetComponent<TextMeshPro>().color = Settings.UsedColor.Value;
            }
        }

        public static class Settings
        {
            public static MelonPreferences_Category MainCategory;

            public static MelonPreferences_Entry<bool> Enabled;

            public static MelonPreferences_Entry<Color> UsedColor;
            public static MelonPreferences_Entry<string> Year;
            public static MelonPreferences_Entry<string> NeonName;
            public static MelonPreferences_Entry<string> Title;
            public static MelonPreferences_Entry<string> Description;
            public static MelonPreferences_Entry<string> Deserves;
            public static MelonPreferences_Entry<string> Job;
            public static MelonPreferences_Entry<string> Sins;

            public static void Register()
            {
                MainCategory = MelonPreferences.CreateCategory("YourStory");

                Enabled = MainCategory.CreateEntry("Enabled", true);

                UsedColor = MainCategory.CreateEntry("Color", Color.white);
                Year = MainCategory.CreateEntry("Year", "1989.");
                NeonName = MainCategory.CreateEntry("Neon Name", "Neon White");
                Title = MainCategory.CreateEntry("Teaser/Title", "As above, so below");
                Description = MainCategory.CreateEntry("Description", """
                A professional killer in his past life.
                saved from eternal punishment
                        White gambles with his humanity
                """);
                Deserves = MainCategory.CreateEntry("Who deserves heaven", """
                WHO
                DESERVES
                A PLACE
                IN
                HEAVEN
                ?
                """);
                Job = MainCategory.CreateEntry("Your job", """
                White and the Neon order
                compete to exterminate
                the demonic invasion of heaven.
                """);
                Sins = MainCategory.CreateEntry("Sin", """
                sin / IDENTITY
                sin / MEMORY
                """);
            }
        }
    }
}
