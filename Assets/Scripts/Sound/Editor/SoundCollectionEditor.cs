using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Sol
{
    [CustomEditor(typeof(SoundCollection))]
    public class SoundCollectionEditor : Editor
    {
        private const string HELP_MESSAGE_PITCH = "Minimum and Maximum pitch control the pitch at which the sound will be played. at 1 the sound will play normally. a random value between the minimum and maximum will be selected when the sound is played. I recommend not going under 0 or over 2.";
        private const string HELP_MESSAGE_INSTANCE_LIMIT = "The instance limit is the number of this particular sound that is allowed to be playing at once. for instance, if the limit is 0 it will never play, while if it is 5 then up to 5 things can play this sound at once.";
        private const string HELP_MESSAGE_AUDIO_CLIP = "This should be the specific clip in the project you want to play when this sound is called. THIS CANNOT BE EMPTY.";
        private const string HELP_MESSAGE_NAME = "This field is mostly for our edification and to help keep sounds organized. Sounds may also be called by name if absolutely necessary so its good to keep the name unique. If two sounds are called EXPLOSION, the sound manager will not know which to play.";
        private const string HELP_MESSAGE_SPATIAL_BLEND = "Sets how much this clip's audiosource is affected by 3D spatialisation calculations (attenuation, doppler etc). 0.0 makes the sound full 2D and distance from the sound won't matter, 1.0 makes it full 3D and distance will matter.";
        private const string HELP_MESSAGE_LOOP = "If looping is disabled, the sound will play only once. if it is enabled, the sound will play continually until stop is explicitly called.";

        private bool showHelp = false;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            showHelp = EditorGUILayout.Toggle("Show Help", showHelp);
            SoundCollection mycollection = (SoundCollection)target;
            if (GUILayout.Button("Add Sound"))
            {
                mycollection.Add();
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            foreach (Sound sound in mycollection.sounds)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.Space();

                if (showHelp) EditorGUILayout.HelpBox(HELP_MESSAGE_NAME, MessageType.None);
                sound.name = EditorGUILayout.TextField("Name ", sound.name);

                EditorGUILayout.Space();

                if (showHelp) EditorGUILayout.HelpBox(HELP_MESSAGE_AUDIO_CLIP, MessageType.None);
                sound.audioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip ", sound.audioClip, typeof(AudioClip), true);

                EditorGUILayout.Space();

                if (showHelp) EditorGUILayout.HelpBox(HELP_MESSAGE_INSTANCE_LIMIT, MessageType.None);
                sound.instanceLimit = EditorGUILayout.IntField("Instance Limit ", sound.instanceLimit);

                EditorGUILayout.Space();

                if(showHelp) EditorGUILayout.HelpBox(HELP_MESSAGE_PITCH, MessageType.None);
                sound.minPitch = EditorGUILayout.FloatField("Minimum Pitch ", sound.minPitch);

                EditorGUILayout.Space();

                sound.maxPitch = EditorGUILayout.FloatField("Max Pitch ", sound.maxPitch);

                EditorGUILayout.Space();

                if (showHelp) EditorGUILayout.HelpBox(HELP_MESSAGE_SPATIAL_BLEND, MessageType.None);
                sound.spatialBlend = EditorGUILayout.FloatField("Spatial Blend ", sound.spatialBlend);

                EditorGUILayout.Space();

                if (showHelp) EditorGUILayout.HelpBox(HELP_MESSAGE_LOOP, MessageType.None);
                sound.loop = EditorGUILayout.Toggle("Loop ", sound.loop);

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

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

