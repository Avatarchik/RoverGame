////////////////////////////////////////////////////////////////////////////
//
//      Name:               tp_Alpha.cs
//      Author:             HOEKKII
//      
//      Description:        This is used for the alpha textures (brushes)
//      
////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TerrainPainter
{
    [Serializable] public class tp_Alpha
    {
        public List<Texture2D> alphaBrushes = new List<Texture2D>();    // All brushes from resources folder
        [SerializeField] public int selectedAlphaBrush = 0;             // Selected brush
        [SerializeField] private bool m_enabled = true;                 // Alpha menu is active and so the projector

        /// <summary>
        /// Is active
        /// </summary>
        public bool Enabled
        {
            get { return m_enabled; }
            set { m_enabled = value; }
        }
        /// <summary>
        /// Get alphamaps
        /// </summary>
        public void Update()
        {
            try
            { // Get all textures in .../Resources/.../TP_Brushes/
                Texture2D[] __texes  = Resources.LoadAll<Texture2D>(TerrainPainter.Settings.BRUSHES_FOLDER_NAME);
                alphaBrushes = __texes.ToList();
            }
            catch (Exception e) { throw new NullReferenceException(TerrainPainter.ErrorMessages.ALPHA_NOT_FOUND + " \n Error code: " + e.ToString()); }
        }

        /// <summary>
        /// Get the current selected texture index
        /// </summary>
        /// <param name="ab">current alpha</param>
        public static explicit operator int (tp_Alpha ab)
        {
            return ab.selectedAlphaBrush;
        }
        /// <summary>
        /// Gets the selected texture
        /// </summary>
        /// <param name="ab">current alpha</param>
        public static explicit operator Texture2D(tp_Alpha ab)
        {
            if (ab.selectedAlphaBrush < 0 || ab.selectedAlphaBrush >= ab.alphaBrushes.Count)
            {
                throw new IndexOutOfRangeException();
            }
            return ab.alphaBrushes[ab.selectedAlphaBrush];
        }
    }
}


