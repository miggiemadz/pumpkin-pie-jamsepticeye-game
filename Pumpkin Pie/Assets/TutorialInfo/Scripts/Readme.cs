using System;
using UnityEngine;

/// <summary>
/// ScriptableObject that stores the contents of the in-editor Readme used by the tutorial assets.
/// This object contains a title, an optional icon, and a collection of sections which are shown
/// in the custom editor UI (ReadmeEditor).
/// </summary>
public class Readme : ScriptableObject
{
    /// <summary>
    /// Optional icon displayed in the Readme header.
    /// </summary>
    public Texture2D icon;

    /// <summary>
    /// Title text displayed in the Readme header.
    /// </summary>
    public string title;

    /// <summary>
    /// An array of content sections presented in the Readme inspector.
    /// Each section contains a heading, body text and an optional link.
    /// </summary>
    public Section[] sections;

    /// <summary>
    /// Tracks whether the tutorial layout has already been loaded for this Readme instance.
    /// This is used by the editor code to only load the example window layout once.
    /// </summary>
    public bool loadedLayout;

    /// <summary>
    /// Represents one content block in the Readme displayed in the inspector.
    /// </summary>
    [Serializable]
    public class Section
    {
        /// <summary>
        /// Heading for the section. Rendered with a larger, bold font in the editor UI.
        /// </summary>
        public string heading;

        /// <summary>
        /// Main body text for the section. Supports simple rich text in the editor.
        /// </summary>
        public string text;

        /// <summary>
        /// Display text used for the selectable link in the inspector.
        /// </summary>
        public string linkText;

        /// <summary>
        /// URL opened when the linkText is clicked in the inspector.
        /// </summary>
        public string url;
    }
}
