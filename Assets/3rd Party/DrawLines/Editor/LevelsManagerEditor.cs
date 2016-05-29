using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

///Created by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

namespace DrawLinesEditors
{
		[CustomEditor(typeof(LevelsManager))]
		public class LevelsManagerEditor : Editor
		{
				private int previousNumberOfRows = -1;
				private int previousNumberOfCols = -1;
				private int dialogResult = 0;
				private Color tempColor;
				private Color greenColor = Color.green;
				private Color whiteColor = Color.white;
				private Color yellowColor = Color.yellow;
				private Color redColor = new Color (255, 0, 0, 255) / 255.0f;
				private Color cyanColor = new Color (0, 255, 255, 255) / 255.0f;
		private Color aluminumSpriteColor = new Color (0, 255, 255, 100) / 255.0f;
		private Color aluminumWireColor = new Color (8, 216, 218, 195) / 255.0f;

		public int wireIndex;
		public Color spriteColor;
		public Color wireColor;
		public Level.WireTypes myWireType;

        public override void OnInspectorGUI()
        {
            LevelsManager attrib = (LevelsManager)target;//get the target

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Instructions :");
            EditorGUILayout.Separator();

            EditorGUILayout.HelpBox("* Select number of Rows and Columns to create a Grid of Size equals [Rows x Columns].", MessageType.None);
            EditorGUILayout.HelpBox("* Click on 'Create New Level' to create a new Level for the Mission", MessageType.None);
            EditorGUILayout.HelpBox("* Click on 'Remove Levels' to remove all Levels in the Mission", MessageType.None);
            EditorGUILayout.HelpBox("* Click on 'View Grid' to show the grid of the Level", MessageType.None);
            EditorGUILayout.HelpBox("* Click on 'Create New Pair' to create a new pair of elements for the Level ", MessageType.None);
            EditorGUILayout.HelpBox("* Click on 'Remove Pairs' to remove all pairs in Level", MessageType.None);
            EditorGUILayout.HelpBox("* Click on 'Remove Level' to remove the Level from the Mission", MessageType.None);
            EditorGUILayout.HelpBox("* Click on 'Remove Pair' to remove the pair from the Level", MessageType.None);
            EditorGUILayout.Separator();
            attrib.numberOfRows = EditorGUILayout.IntSlider("Number of Rows", attrib.numberOfRows, 2, LevelsManager.rowsLimit);
            EditorGUILayout.Separator();
            attrib.numberOfCols = EditorGUILayout.IntSlider("Number of Columns", attrib.numberOfCols, 2, LevelsManager.colsLimit);
            EditorGUILayout.Separator();

            attrib.defaultSprite = EditorGUILayout.ObjectField("Default Sprite", attrib.defaultSprite, typeof(Sprite), true) as Sprite;
            EditorGUILayout.Separator();

            attrib.createRandomColor = EditorGUILayout.Toggle("Create Random Color", attrib.createRandomColor);
            EditorGUILayout.Separator();

            if (previousNumberOfRows == -1)
            {
                previousNumberOfRows = attrib.numberOfRows;
            }

            if (previousNumberOfCols == -1)
            {
                previousNumberOfCols = attrib.numberOfCols;
            }

            if (previousNumberOfCols != attrib.numberOfCols || previousNumberOfRows != attrib.numberOfRows)
            {

                if (attrib.levels.Count != 0)
                {
                    dialogResult = EditorUtility.DisplayDialogComplex("Confirm Message", "Changing grid size leads to reset all levels", "ok", "cancel", "close");
                    if (dialogResult == 0)
                    {
                        RemoveLevels(attrib);
                    }
                    else {
                        attrib.numberOfRows = previousNumberOfRows;
                        attrib.numberOfCols = previousNumberOfCols;
                    }
                }
                else {
                    RemoveLevels(attrib);
                }
            }

            GUILayout.BeginHorizontal();

            GUI.backgroundColor = greenColor;
            if (GUILayout.Button("Create New Level", GUILayout.Width(150), GUILayout.Height(30)))
            {
				
                CreateNewLevel(attrib);
            }
            GUI.backgroundColor = whiteColor;

            if (attrib.levels.Count != 0)
            {
                GUI.backgroundColor = redColor;
                if (GUILayout.Button("Remove Levels", GUILayout.Width(150), GUILayout.Height(30)))
                {
                    dialogResult = EditorUtility.DisplayDialogComplex("Removing Levels", "Are you sure you want to remove all levels ?", "yes", "cancel", "close");
                    if (dialogResult == 0)
                    {
                        RemoveLevels(attrib);
                    }
                }
                GUI.backgroundColor = whiteColor;
            }

            GUILayout.EndHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            for (int i = 0; i < attrib.levels.Count; i++)
            {
                GUI.contentColor = yellowColor;
                attrib.levels[i].showLevel = EditorGUILayout.Foldout(attrib.levels[i].showLevel, " [Level " + (i + 1) + "]");
                GUI.contentColor = whiteColor;

                if (attrib.levels[i].showLevel)
                {
                    EditorGUILayout.Separator();
                    GUILayout.BeginVertical();
                    attrib.levels[i].showPairsNumber = EditorGUILayout.Toggle("Show Pairs Number", attrib.levels[i].showPairsNumber);
                    EditorGUILayout.Separator();

                    GUILayout.EndVertical();


                    GUI.backgroundColor = cyanColor;
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("View Grid", GUILayout.Width(360), GUILayout.Height(30)))
                    {
                        DrawLinesEditors.GridWindowEditor.Init(attrib.levels[i], "Level " + (i + 1) + " Grid", attrib.numberOfRows, attrib.numberOfCols);
                    }
                    GUI.backgroundColor = whiteColor;

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUI.backgroundColor = greenColor;

                    if (GUILayout.Button("Create New Pair", GUILayout.Width(110), GUILayout.Height(30)))
                    {
                        if (attrib.levels[i].dotsPairs.Count < attrib.numberOfRows * attrib.numberOfCols / 2)
                        {
                            CreateNewPair(attrib, attrib.levels[i]);
                        }
                        else {
                            EditorUtility.DisplayDialog("Limit Reached", "You can't add more pairs", "ok");
                        }
                    }

                    if (GUILayout.Button("Create New Barrier", GUILayout.Width(110), GUILayout.Height(30)))
                    {
                        if (attrib.levels[i].dotsPairs.Count < attrib.numberOfRows * attrib.numberOfCols / 2)
                        {
                            CreateNewBarrier(attrib, attrib.levels[i]);
                        }
                        else {
                            EditorUtility.DisplayDialog("Limit Reached", "You can't add more pairs", "ok");
                        }
                    }
                    GUI.backgroundColor = whiteColor;

                    GUI.backgroundColor = redColor;
                    if (GUILayout.Button("Remove Pairs", GUILayout.Width(120), GUILayout.Height(30)))
                    {
                        dialogResult = EditorUtility.DisplayDialogComplex("Removing Level Pairs", "Are you sure you want to remove the pairs of Level" + (i + 1) + " ?", "yes", "cancel", "close");
                        if (dialogResult == 0)
                        {
                            RemoveLevelPairs(attrib.levels[i], attrib);
                            continue;
                        }
                    }
                    GUI.backgroundColor = whiteColor;

                    GUI.backgroundColor = redColor;
                    if (GUILayout.Button("Remove Level " + (i + 1), GUILayout.Width(120), GUILayout.Height(30)))
                    {
                        dialogResult = EditorUtility.DisplayDialogComplex("Removing Level", "Are you sure you want to remove level " + (i + 1) + " ?", "yes", "cancel", "close");
                        if (dialogResult == 0)
                        {
                            RemoveLevel(i, attrib);
                            continue;
                        }
                    }
                    GUI.backgroundColor = whiteColor;

                    GUILayout.EndHorizontal();


                    EditorGUILayout.Separator();
                    GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(2));

                    EditorGUILayout.Separator();

                    for (int j = 0; j < attrib.levels[i].dotsPairs.Count; j++)
                    {
                        attrib.levels[i].dotsPairs[j].showPair = EditorGUILayout.Foldout(attrib.levels[i].dotsPairs[j].showPair, "Pair " + (j + 1));

                        if (attrib.levels[i].dotsPairs[j].showPair)
                        {

                            GUI.backgroundColor = redColor;
                            if (GUILayout.Button("Remove Pair " + (j + 1), GUILayout.Width(120), GUILayout.Height(25)))
                            {
                                dialogResult = EditorUtility.DisplayDialogComplex("Removing Pair", "Are you sure you want to remove pair " + (j + 1) + " ?", "yes", "cancel", "close");
                                if (dialogResult == 0)
                                {
                                    RemovePair(j, attrib.levels[i], attrib);
                                    continue;
                                }
                            }
                            GUI.backgroundColor = whiteColor;

                            EditorGUILayout.Separator();
                            if (attrib.levels[i].dotsPairs[j].sprite == null)
                            {
                                attrib.levels[i].dotsPairs[j].sprite = attrib.defaultSprite;
                            }

                            if (attrib.levels[i].dotsPairs[j].connectSprite == null)
                            {
                                attrib.levels[i].dotsPairs[j].connectSprite = attrib.levels[i].dotsPairs[j].sprite;
                            }

                            attrib.levels[i].dotsPairs[j].sprite = EditorGUILayout.ObjectField("Normal Sprite", attrib.levels[i].dotsPairs[j].sprite, typeof(Sprite), true) as Sprite;
                            EditorGUILayout.Separator();

                            attrib.levels[i].dotsPairs[j].connectSprite = EditorGUILayout.ObjectField("OnConnect Sprite", attrib.levels[i].dotsPairs[j].connectSprite, typeof(Sprite), true) as Sprite;
                            EditorGUILayout.Separator();

							attrib.levels [i].dotsPairs [j].wireIndex = EditorGUILayout.Popup(attrib.levels [i].dotsPairs [j].wireIndex, new string[] {"Aluminum", "Copper", "Gold", "Silver"});

							switch (attrib.levels [i].dotsPairs [j].wireIndex) {
							case 0:
								myWireType = Level.WireTypes.Aluminum;
								spriteColor = aluminumSpriteColor;
								wireColor = aluminumWireColor;
								break;
							case 1:
								myWireType = Level.WireTypes.Copper;
								wireColor = Color.magenta;
								break;
							case 2:
								myWireType = Level.WireTypes.Gold;
								wireColor = Color.yellow;
								break;
							case 3:
								myWireType = Level.WireTypes.Silver;
								wireColor = Color.gray;
								break;
							default:
								break;
							}
							EditorGUILayout.Separator();
							attrib.levels [i].dotsPairs [j].wireType = myWireType;
							attrib.levels [i].dotsPairs [j].color = spriteColor;
							attrib.levels [i].dotsPairs [j].lineColor = wireColor;

                            attrib.levels[i].dotsPairs[j].color = EditorGUILayout.ColorField("Sprite Color", attrib.levels[i].dotsPairs[j].color);
                            EditorGUILayout.Separator();

                            attrib.levels[i].dotsPairs[j].lineColor = EditorGUILayout.ColorField("Line Color", attrib.levels[i].dotsPairs[j].lineColor);
                            EditorGUILayout.Separator();

                            attrib.levels[i].dotsPairs[j].applyColorOnSprite = EditorGUILayout.Toggle("Apply Color On Sprite", attrib.levels[i].dotsPairs[j].applyColorOnSprite);
                            EditorGUILayout.Separator();

                            attrib.levels[i].dotsPairs[j].firstDot.index = EditorGUILayout.IntSlider("First Element Index", attrib.levels[i].dotsPairs[j].firstDot.index, 0, attrib.numberOfRows * attrib.numberOfCols - 1);
                            EditorGUILayout.Separator();
                            attrib.levels[i].dotsPairs[j].secondDot.index = EditorGUILayout.IntSlider("Second Element Index", attrib.levels[i].dotsPairs[j].secondDot.index, 0, attrib.numberOfRows * attrib.numberOfCols - 1);
							EditorGUILayout.Separator();
						}
                    }


                    for (int j = 0; j < attrib.levels[i].barriers.Count; j++)
                    {
                        attrib.levels[i].barriers[j].showPair = EditorGUILayout.Foldout(attrib.levels[i].barriers[j].showPair, "Barrier " + (j + 1));

                        if (attrib.levels[i].barriers[j].showPair)
                        {
                            GUI.backgroundColor = redColor;
                            if (GUILayout.Button("Remove Barrier " + (j + 1), GUILayout.Width(120), GUILayout.Height(25)))
                            {
                                dialogResult = EditorUtility.DisplayDialogComplex("Removing Barrier", "Are you sure you want to remove barrier " + (j + 1) + " ?", "yes ;)", "cancel", "close");
                                if (dialogResult == 0)
                                {
                                    RemoveBarrier(j, attrib.levels[i], attrib);
                                    continue;
                                }
                            }
                            GUI.backgroundColor = whiteColor;

                            EditorGUILayout.Separator();
                            if (attrib.levels[i].barriers[j].sprite == null)
                            {
                                attrib.levels[i].barriers[j].sprite = attrib.defaultSprite;
                            }

                            attrib.levels[i].barriers[j].sprite = EditorGUILayout.ObjectField("Normal Sprite", attrib.levels[i].barriers[j].sprite, typeof(Sprite), true) as Sprite;
                            EditorGUILayout.Separator();

                            attrib.levels[i].barriers[j].color = EditorGUILayout.ColorField("Sprite Color", attrib.levels[i].barriers[j].color);
                            EditorGUILayout.Separator();

                            attrib.levels[i].barriers[j].index = EditorGUILayout.IntSlider("Barrier Index", attrib.levels[i].barriers[j].index, 0, attrib.numberOfRows * attrib.numberOfCols - 1);
                        }
                    }
                }
            }
        }

				private void CreateNewLevel (LevelsManager attrib)
				{
						if (attrib == null) {
								return;
						}

						Level lvl = new Level ();
						attrib.levels.Add (lvl);
				}

				private void CreateNewPair (LevelsManager attrib, Level lvl)
				{
						if (lvl == null) {
								return;
						}


						if (attrib.createRandomColor) {
								tempColor = new Color (Random.Range (0, 255), Random.Range (0, 255), Random.Range (0, 255), 255) / 255.0f;
						} else {
								tempColor = whiteColor;
						}

						lvl.dotsPairs.Add (new Level.DotsPair () { color = tempColor, lineColor = tempColor });
				}

        private void CreateNewBarrier(LevelsManager attrib, Level lvl)
        {
            if (lvl == null)
            {
                return;
            }


            if (attrib.createRandomColor)
            {
                tempColor = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), 255) / 255.0f;
            }
            else {
                tempColor = whiteColor;
            }

            lvl.barriers.Add(new Level.Barrier() { color = tempColor });
        }

        private void RemoveLevels (LevelsManager attrib)
				{
						if (attrib == null) {
								return;
						}
						attrib.levels.Clear ();
						previousNumberOfRows = attrib.numberOfRows;
						previousNumberOfCols = attrib.numberOfCols;
				}

				private void RemoveLevel (int index, LevelsManager attrib)
				{

						if (!(index >= 0 && index < attrib.levels.Count)) {
								return;
						}

						attrib.levels.RemoveAt (index);
				}

				private void RemoveLevelPairs (Level level, LevelsManager attrib)
				{
						if (level == null) {
								return;
						}
						level.dotsPairs.Clear ();
				}

				private void RemovePair (int index, Level level, LevelsManager attrib)
				{
						if (level == null) {
								return;
						}

						if (!(index >= 0 && index < level.dotsPairs.Count)) {
								return;
						}

						level.dotsPairs.RemoveAt (index);
				}


        private void RemoveBarrier(int index, Level level, LevelsManager attrib)
        {
            if (level == null)
            {
                return;
            }

            if (!(index >= 0 && index < level.barriers.Count))
            {
                return;
            }

            level.barriers.RemoveAt(index);
        }
    }
}