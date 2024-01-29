﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Tuna.Revit.Extension;

[Transaction(TransactionMode.Manual)]
[CommandButton(Title = "sd")]
internal class TestCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIDocument uiDocument = commandData.Application.ActiveUIDocument;
        Document document = uiDocument.Document;

        var result = uiDocument.SelectObjects(Autodesk.Revit.UI.Selection.ObjectType.Element, prompt: "asd", pPreSelected: new List<Reference>()
        {
            new Reference(document.GetElement(new ElementId(2312)))
        });
        if (result.Succeeded)
        {
            foreach (var item in result.Value)
            {
                System.Windows.MessageBox.Show(item.ElementId.ToString());
            }
        }


        return Result.Succeeded;
    }
}

