using UnityEngine;
using UnityEditor;
using System;


//Just draw the base materialEditor for now, as we can add all of our properties from the shader itself
//This way we can also hide properties we don't want to see
public class ShaderViewer : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        foreach (MaterialProperty property in properties)
            materialEditor.ShaderProperty(property, property.displayName);
    }
}