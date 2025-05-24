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
using NeonLite.Modules;
using NeonLite;

namespace YourStory
{
    [HarmonyPatch]
    public class YourStory : MelonMod, IModule
    {
        const bool priority = true;
        static bool active = true;

        public override void OnInitializeMelon()
        {
            NeonLite.NeonLite.LoadModules(MelonAssembly);
        }

        static GameObject asset;
        static AssetBundleCreateRequest bundleLoad;

        static bool dirty = false;

        public override void OnLateInitializeMelon()
        {
            bundleLoad = AssetBundle.LoadFromMemoryAsync(Resources.Resources.AssetBundle);
            bundleLoad.completed += OnBundleLoaded;
        }

        private void OnBundleLoaded(AsyncOperation obj)
        {
            asset = UnityEngine.Object.Instantiate(bundleLoad.assetBundle.LoadAsset<GameObject>("Assets/YourStory.prefab"));
            asset.name = "YourStory";
            asset.gameObject.SetActive(false);
            UnityEngine.Object.DontDestroyOnLoad(asset);
            SetupAsset();
        }

        static void SetupAsset()
        {
            var scaler = asset.transform.GetChild(0);

            void SetText(int id, string text)
            {
                var t = scaler.GetChild(id).GetComponent<TextMeshPro>();
                t.text = text;
                t.color = Settings.UsedColor.Value;
            }

            SetText(0, Settings.Year.Value);
            SetText(1, Settings.NeonName.Value);
            SetText(2, Settings.Title.Value);
            SetText(3, Settings.Description.Value);
            SetText(4, Settings.Deserves.Value);
            SetText(5, Settings.Job.Value);
            SetText(6, Settings.Sins.Value);

            dirty = false;
        }

        public static void Setup()
        {
            Settings.Register();
            Settings.Enabled.SetupForModule(Activate, static (_, after) => after);
        }

        static void Activate(bool activate) => active = activate;

        public static void OnLevelLoad(LevelData level)
        {
            if (!level || level.type == LevelData.LevelType.Hub)
                return;

            var ogBackstory = RM.ui.portraitUI.transform.parent.Find("Backstory");
            var child0 = ogBackstory.GetChild(0);
            child0.gameObject.SetActive(false);

            if (dirty)
                SetupAsset();

            var newBackstory = Utils.InstantiateUI(asset, "YourStory", ogBackstory).transform;
            newBackstory.position = child0.position;
            newBackstory.localScale = child0.localScale;
            newBackstory.gameObject.SetActive(true);
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

                UsedColor.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
                Year.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
                NeonName.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
                Title.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
                Description.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
                Deserves.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
                Job.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
                Sins.OnEntryValueChanged.Subscribe((_, _) => dirty = true);
            }
        }
    }
}
