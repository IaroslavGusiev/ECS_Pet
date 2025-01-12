#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Project.Editor
{
    [InitializeOnLoad]
    public class EditorBootstrapper
    {
        private const string PreviousSceneKey = "PreviousScene";
        private const string ShouldLoadBootstrapKey = "LoadBootstrapScene";
        
        private const string LoadBootstrapMenu = "Tools/Load Bootstrap Scene On Play";
        private const string DontLoadBootstrapMenu = "Tools/Don't Load Bootstrap Scene On Play";
        
        private static string BootstrapScene => EditorBuildSettings.scenes[0].path;
        
        private static string PreviousScene
        {
            get => EditorPrefs.GetString(PreviousSceneKey);
            set => EditorPrefs.SetString(PreviousSceneKey, value);
        }
        
        private static bool ShouldLoadBootstrapScene
        {
            get => EditorPrefs.GetBool(ShouldLoadBootstrapKey, true);
            set => EditorPrefs.SetBool(ShouldLoadBootstrapKey, value);
        }
        
        static EditorBootstrapper() => 
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        
        private static void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            if (ShouldLoadBootstrapScene == false)
            {
                return;
            }

            switch (playModeStateChange)
            {
                case PlayModeStateChange.ExitingEditMode:

                    PreviousScene = SceneManager.GetActiveScene().path;
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() && IsSceneInBuildSettings(BootstrapScene))
                    {
                        EditorSceneManager.OpenScene(BootstrapScene);
                    }
                    break;
                
                case PlayModeStateChange.EnteredEditMode:
                    if (string.IsNullOrEmpty(PreviousScene) == false)
                    {
                        EditorSceneManager.OpenScene(PreviousScene);
                    }
                    break;
            }
        }
        
        [MenuItem(LoadBootstrapMenu)]
        private static void EnableBootstrapper() => 
            ShouldLoadBootstrapScene = true;

        [MenuItem(DontLoadBootstrapMenu)]
        private static void DisableBootstrapper() => 
            ShouldLoadBootstrapScene = false;

        [MenuItem(LoadBootstrapMenu, isValidateFunction: true)]
        private static bool ValidateEnableBootstrapper()
        {
            Menu.SetChecked(LoadBootstrapMenu, ShouldLoadBootstrapScene);
            return ShouldLoadBootstrapScene == false;
        }

        [MenuItem(DontLoadBootstrapMenu, isValidateFunction: true)]
        private static bool ValidateDisableBootstrapper()
        {
            Menu.SetChecked(DontLoadBootstrapMenu, ShouldLoadBootstrapScene == false);
            return ShouldLoadBootstrapScene;
        }
        
        private static bool IsSceneInBuildSettings(string scenePath) => 
            string.IsNullOrEmpty(scenePath) == false && EditorBuildSettings.scenes.Any(scene => scene.path == scenePath);
    }
}

#endif