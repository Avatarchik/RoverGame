using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Sol
{
    [CustomEditor(typeof(SoundCollection))]
    public class SoundCollectionEditor : Editor
    {
        private const string HELP_MESSAGE_PITCH = "Minimum and Maximum pitch control the pitch at which the sound will be played. at 1 the sound will play normally. a random value between the minimum and maximum will be selected when the sound is played. I recommend not going under 0 or over 2.";
        private const string HELP_MESSAGE_VOLUME = "The volume determines the volume at which the sound will be played. At 0 the sound will be muted entirely, at 1 it will be played as loud as the audioclip will allow.";
        private const string HELP_MESSAGE_INSTANCE_LIMIT = "The instance limit is the number of this particular sound that is allowed to be playing at once. for instance, if the limit is 0 it will never play, while if it is 5 then up to 5 things can play this sound at once.";
        private const string HELP_MESSAGE_AUDIO_CLIP = "This should be the specific clip in the project you want to play when this sound is called. THIS CANNOT BE EMPTY.";
        private const string HELP_MESSAGE_NAME = "This field is mostly for our edification and to help keep sounds organized. Sounds may also be called by name if absolutely necessary so its good to keep the name unique. If two sounds are called EXPLOSION, the sound manager will not know which to play.";
        private const string HELP_MESSAGE_SPATIAL_BLEND = "Sets how much this clip's audiosource is affected by 3D spatialisation calculations (attenuation, doppler etc). 0.0 makes the sound full 2D and distance from the sound won't matter, 1.0 makes it full 3D and distance will matter.";
        private const string HELP_MESSAGE_LOOP = "If looping is disabled, the sound will play only once. if it is enabled, the sound will play continually until stop is explicitly called.";


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SoundCollection mycollection = (SoundCollection)target;
            if (GUILayout.Button("Add New Sound"))
            {
                mycollection.Add();
            }

            GUI.color = Color.red;
            if (GUILayout.Button("Clear Sounds"))
            {
                if (EditorUtility.DisplayDialog("Clear All Sounds",
            "Are sure you want to clear all the sounds? This action cannot be undone.", "Yes Damnit  (╯°□°)╯︵ ┻━┻", "Oh god no"))
                {
                    mycollection.sounds.Clear();
                }
            }
            GUI.color = Color.white;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            foreach (Sound sound in mycollection.sounds)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.Space();

                GUIContent content = new GUIContent("Name ", HELP_MESSAGE_NAME);
                sound.name = EditorGUILayout.TextField(content, sound.name);

                EditorGUILayout.Space();

                content = new GUIContent("Audio Clip ", HELP_MESSAGE_AUDIO_CLIP);
                sound.audioClip = (AudioClip)EditorGUILayout.ObjectField(content, sound.audioClip, typeof(AudioClip), true);

                EditorGUILayout.Space();

                content = new GUIContent("Volume ", HELP_MESSAGE_VOLUME);
                sound.volume = EditorGUILayout.Slider(content, sound.volume, 0f, 1f);

                EditorGUILayout.Space();

                content = new GUIContent("Instance Limit ", HELP_MESSAGE_INSTANCE_LIMIT);
                sound.instanceLimit = EditorGUILayout.IntField(content, sound.instanceLimit);
                
                
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                content = new GUIContent("Pitch ", HELP_MESSAGE_PITCH);
                EditorGUILayout.LabelField(content);
                float minVal = sound.minPitch;
                float maxVal = sound.maxPitch;
                EditorGUILayout.MinMaxSlider(ref minVal, ref maxVal, 0f, 3f);
                sound.minPitch = minVal;
                sound.maxPitch = maxVal;


                EditorGUILayout.BeginHorizontal();
                float width = EditorGUIUtility.currentViewWidth - 50;

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("min", GUILayout.Width( width/2));
                sound.minPitch = EditorGUILayout.FloatField(sound.minPitch, GUILayout.Width(width / 2));
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("max", GUILayout.Width(width / 2));
                sound.maxPitch = EditorGUILayout.FloatField(sound.maxPitch, GUILayout.Width(width / 2));
                EditorGUILayout.EndVertical();

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                content = new GUIContent("Spatial Blend ", HELP_MESSAGE_SPATIAL_BLEND);
                sound.spatialBlend = EditorGUILayout.Slider(content, sound.spatialBlend, 0f, 1f);

                EditorGUILayout.Space();

                content = new GUIContent("Loop ", HELP_MESSAGE_LOOP);
                sound.loop = EditorGUILayout.Toggle(content, sound.loop);

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (GUILayout.Button("Reset"))
                {
                    mycollection.Reset(sound);
                    break;
                }

                if (GUILayout.Button("Remove"))
                {
                    mycollection.Remove(sound);
                    break;
                }

                EditorGUILayout.EndVertical();

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }
        }
    }
}

