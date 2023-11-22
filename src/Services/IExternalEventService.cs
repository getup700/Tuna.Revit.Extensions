﻿using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuna.Revit.Extension.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExternalEventService
    {
        /// <summary>
        /// 
        /// </summary>
        Task PostCommandAsync(Action<UIApplication> handle);
    }
}
