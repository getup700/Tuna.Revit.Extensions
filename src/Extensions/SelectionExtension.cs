﻿///************************************************************************************
///   Author:十五
///   CretaeTime:2022/11/29 0:08:10
///   Mail:1012201478@qq.com
///   Github:https://github.com/shichuyibushishiwu
///
///   Description:
///
///************************************************************************************

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuna.Revit.Extension
{
    public static class SelectionExtension
    {
        /// <summary>
        /// Prompts the user to select one element , if the user cancels the operation (for example, through ESC), the method will return null. 
        /// </summary>
        /// <param name="uiDocument"></param>
        /// <returns></returns>
        public static Element SelectElement(this UIDocument uiDocument)
        {
            Reference elementRef = uiDocument.SelectObject(Autodesk.Revit.UI.Selection.ObjectType.Element);

            if (elementRef == null)
            {
                return default;
            }
            return uiDocument.Document.GetElement(elementRef);
        }

        /// <summary>
        /// Prompts the user to select one object , if the user cancels the operation (for example, through ESC), the method will return null. 
        /// </summary>
        /// <param name="uiDocument"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Reference SelectObject(this UIDocument uiDocument, Autodesk.Revit.UI.Selection.ObjectType objectType)
        {
            if (uiDocument == null)
            {
                throw new ArgumentNullException("UIDocument can not be null");
            }

            Reference reference = null;
            try
            {
                reference = uiDocument.Selection.PickObject(objectType);
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException exception)
            {
                string logInfo = exception.Message;
            }

            return reference;
        }
    }
}