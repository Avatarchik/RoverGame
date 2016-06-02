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
		private Color aluminumColor = new Color (0, 255, 170, 255) / 255.0f;
		private Color aluminumOnConnectColor = new Color (0, 255, 170, 115) / 255.0f;
		private Color aluminumOnConnectBGColor = new Color (0, 255, 255, 125) / 255.0f;
		private Color aluminumWireColor = new Color (0, 255, 210, 220) / 255.0f;
		private Color copperColor = new Color (232, 115, 52, 255) / 255.0f;
		private Color copperOnConnectColor = new Color (232, 114, 52, 115) / 255.0f;
		private Color copperOnConnectBGColor = new Color (255, 160, 0, 125) / 255.0f;
		private Color copperWireColor = new Color (232, 114, 52, 220) / 255.0f;
		private Color goldColor = new Color (255, 200, 0, 255) / 255.0f;
		private Color goldOnConnectColor = new Color (255, 200, 0, 115) / 255.0f;
		private Color goldOnConnectBGColor = new Color (255, 225, 0, 125) / 255.0f;
		private Color goldWireColor = new Color (255, 233, 0, 220) / 255.0f;
		private Color silverColor = new Color (134, 179, 186, 255) / 255.0f;
		private Color silverOnConnectColor = new Color (134, 179, 186, 115) / 255.0f;
		private Color silverOnConnectBGColor = new Color (133, 195, 223, 125) / 255.0f;
		private Color silverWireColor = new Color (154, 205, 213, 220) / 255.0f;

		public int wireIndex;
		public Color spriteColor;
		public Color onConnectColor;
		public Color onConnectBGColor;
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

            attrib.defaultPairSprite = EditorGUILayout.ObjectField("Default Pair Sprite", attrib.defaultPairSprite, typeof(Sprite), true) as Sprite;
            EditorGUILayout.Separator();

			attrib.defaultOnConnectPairSprite = EditorGUILayout.ObjectField("Default Pair Sprite", attrib.defaultOnConnectPairSprite, typeof(Sprite), true) as Sprite;
			EditorGUILayout.Separator();

			attrib.defaultOnConnectBGSprite = EditorGUILayout.ObjectField("Default Background Sprite", attrib.defaultOnConnectBGSprite, typeof(Sprite), true) as Sprite;
			EditorGUILayout.Separator();

			attrib.defaultBarrierSprite = EditorGUILayout.ObjectField("Default Barrier Sprite", attrib.defaultBarrierSprite, typeof(Sprite), true) as Sprite;
			EditorGUILayout.Separator();

			attrib.defaultPairs = EditorGUILayout.Toggle("Set Pair Sprites To Default", attrib.defaultPairs);
			EditorGUILayout.Separator();

			attrib.defaulOnConnectPairs = EditorGUILayout.Toggle("Set On Connect Sprites To Default", attrib.defaulOnConnectPairs);
			EditorGUILayout.Separator();

			attrib.defaulOnConnectBGs = EditorGUILayout.Toggle("Set On Connect Background Sprites To Default", attrib.defaulOnConnectBGs);
			EditorGUILayout.Separator();

			attrib.defaultBarriers = EditorGUILayout.Toggle("Set Barrier Sprites To Default", attrib.defaultBarriers);
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
							if (attrib.levels[i].dotsPairs[j].pairSprite == null || attrib.defaultPairs)
                            {
								attrib.levels[i].dotsPairs[j].pairSprite = attrib.defaultPairSprite;
                            }

							if (attrib.levels[i].dotsPairs[j].connectSprite == null || attrib.defaulOnConnectPairs)
                            {
								attrib.levels[i].dotsPairs[j].connectSprite = attrib.defaultOnConnectPairSprite;
                            }

							if (attrib.levels[i].dotsPairs[j].connectBGSprite == null || attrib.defaulOnConnectBGs)
							{
								attrib.levels[i].dotsPairs[j].connectBGSprite = attrib.defaultOnConnectBGSprite;
							}

							attrib.levels[i].dotsPairs[j].pairSprite = EditorGUILayout.ObjectField("Normal Sprite", attrib.levels[i].dotsPairs[j].pairSprite, typeof(Sprite), true) as Sprite;
                            EditorGUILayout.Separator();

                            attrib.levels[i].dotsPairs[j].connectSprite = EditorGUILayout.ObjectField("OnConnect Sprite", attrib.levels[i].dotsPairs[j].connectSprite, typeof(Sprite), true) as Sprite;
                            EditorGUILayout.Separator();

							attrib.levels[i].dotsPairs[j].connectBGSprite = EditorGUILayout.ObjectField("OnConnect Background Sprite", attrib.levels[i].dotsPairs[j].connectBGSprite, typeof(Sprite), true) as Sprite;
							EditorGUILayout.Separator();

							attrib.levels [i].dotsPairs [j].wireIndex = EditorGUILayout.Popup(attrib.levels [i].dotsPairs [j].wireIndex, new string[] {"Aluminum", "Copper", "Gold", "Silver"});

							switch (attrib.levels [i].dotsPairs [j].wireIndex) {
							case 0:
								myWireType = Level.WireTypes.Aluminum;
								spriteColor = aluminumColor;
								onConnectColor = aluminumOnConnectColor;
								onConnectBGColor = aluminumOnConnectBGColor;
								wireColor = aluminumWireColor;
								break;
							case 1:
								myWireType = Level.WireTypes.Copper;
								spriteColor = copperColor;
								onConnectColor = copperOnConnectColor;
								onConnectBGColor = copperOnConnectBGColor;
								wireColor = copperWireColor;
								break;
							case 2:
								myWireType = Level.WireTypes.Gold;
								spriteColor = goldColor;
								onConnectColor = goldOnConnectColor;
								onConnectBGColor = goldOnConnectBGColor;
								wireColor = goldWireColor;
								break;
							case 3:
								myWireType = Level.WireTypes.Silver;
								spriteColor = silverColor;
								onConnectColor = silverOnConnectColor;
								onConnectBGColor = silverOnConnectBGColor;
								wireColor = silverWireColor;
								break;
							default:
								break;
							}
							EditorGUILayout.Separator();
							attrib.levels [i].dotsPairs [j].wireType = myWireType;
							attrib.levels [i].dotsPairs [j].pairColor = spriteColor;
							attrib.levels [i].dotsPairs [j].onConnectColor = onConnectColor;
							attrib.levels [i].dotsPairs [j].bgColor = onConnectBGColor;
							attrib.levels [i].dotsPairs [j].lineColor = wireColor;

							attrib.levels[i].dotsPairs[j].pairColor = EditorGUILayout.ColorField("Pair Sprite Color", attrib.levels[i].dotsPairs[j].pairColor);
                            EditorGUILayout.Separator();

							attrib.levels[i].dotsPairs[j].onConnectColor = EditorGUILayout.ColorField("On Connect Color", attrib.levels[i].dotsPairs[j].onConnectColor);
							EditorGUILayout.Separator();

							attrib.levels[i].dotsPairs[j].bgColor = EditorGUILayout.ColorField("Background Color", attrib.levels[i].dotsPairs[j].bgColor);
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
							if (attrib.levels[i].barriers[j].sprite == null || attrib.defaultBarriers)
                            {
                                attrib.levels[i].barriers[j].sprite = attrib.defaultBarrierSprite;
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

			lvl.dotsPairs.Add (new Level.DotsPair () { pairColor = tempColor, lineColor = tempColor });
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