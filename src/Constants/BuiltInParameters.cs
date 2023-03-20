﻿///************************************************************************************
///   Author:十五
///   CretaeTime:2023/3/9 0:30:51
///   Mail:1012201478@qq.com
///   Github:https://github.com/shichuyibushishiwu
///
///   Description:
///
///************************************************************************************

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuna.Revit.Extension.Constants
{
    public class BuiltInParameters
    {
        public static ElementId Invaild => new ElementId(BuiltInParameter.INVALID);

        public class View
        {
            public static ElementId Name => new ElementId(BuiltInParameter.VIEW_NAME);

            public static ElementId Description => new ElementId(BuiltInParameter.VIEW_DESCRIPTION);

            public static ElementId Type => new ElementId(BuiltInParameter.VIEW_TYPE);

            public static ElementId Scale => new ElementId(BuiltInParameter.VIEW_SCALE);

        }

        public class Sheet
        {
            public static ElementId Name => new ElementId(BuiltInParameter.SHEET_NAME);

            public static ElementId Number => new ElementId(BuiltInParameter.SHEET_NUMBER);

            public static ElementId Scale => new ElementId(BuiltInParameter.SHEET_SCALE);

            public static ElementId Date => new ElementId(BuiltInParameter.SHEET_DATE);
        }

        public class Level
        {
            public static ElementId Name => new ElementId(BuiltInParameter.LEVEL_NAME);
        }

        public class Symbol
        {
            public static ElementId Name => new ElementId(BuiltInParameter.SYMBOL_NAME_PARAM);
        }

        public class Room
        {
            public static ElementId Name => new ElementId(BuiltInParameter.ROOM_NAME);

            public static ElementId Number => new ElementId(BuiltInParameter.ROOM_NUMBER);

            public static ElementId Area => new ElementId(BuiltInParameter.ROOM_AREA);
        }
    }
}