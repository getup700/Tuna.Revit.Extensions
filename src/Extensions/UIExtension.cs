﻿/************************************************************************************
   Author:十五
   CretaeTime:2022/4/20 13:16:40
   Mail:1012201478@qq.com
   Github:https://github.com/shichuyibushishiwu

   Description:

************************************************************************************/


using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Tuna.Revit.Extension;

/// <summary>
/// Revit ribbon ui extensions
/// </summary>
public static class UIExtension
{
    static PushButtonData CreatePushButtonData<T>(Action<PushButtonData> handle = null) where T : class, IExternalCommand, new()
    {
        //按钮的类型
        Type commandType = typeof(T);

        //按钮的名称
        string name = commandType.Name;

        //实例化一个按钮的数据
        PushButtonData pushButtonData = new($"btn_{name}", name ?? name, commandType.Assembly.Location, commandType.FullName);

        //如果命令实现了 IExternalCommandAvailability 接口
        if (typeof(IExternalCommandAvailability).IsAssignableFrom(commandType))
        {
            pushButtonData.AvailabilityClassName = commandType.FullName;
        }

        //方式三，通过属性进行配置，优先级第三
        CommandButtonAttribute command = commandType.GetCustomAttribute<CommandButtonAttribute>();
        if (command != null)
        {
            SetPushButtonData(pushButtonData, command);
        }

        //方式二，通过接口进行配置，优先级第二
        if (typeof(IRibbonButtonData).IsAssignableFrom(commandType))
        {
            SetPushButtonData(pushButtonData, Activator.CreateInstance<T>() as IRibbonButtonData);
        }

        //方式一，通过回调进行配置，优先级第一
        handle?.Invoke(pushButtonData);

        return pushButtonData;

        static void SetPushButtonData(PushButtonData pushButtonData, IRibbonButtonData button)
        {
            pushButtonData.Text = button.Title;
            pushButtonData.Image = ImageResolve(button.Image);
            pushButtonData.LargeImage = ImageResolve(button.LargeImage);
            pushButtonData.ToolTipImage = ImageResolve(button.ToolTipImage);
            pushButtonData.ToolTip = button.ToolTip;
            pushButtonData.LongDescription = button.LongDescription;
            pushButtonData.SetContextualHelp(button.ContextualHelp);
        }

        static ImageSource ImageResolve(object source) => source switch
        {
            string path => new BitmapImage(new Uri(path)),
            Bitmap bitmap => bitmap.ConvertToBitmapSource(),
            ImageSource imageSource => imageSource,
            _ => default,
        };
    }

    /// <summary>
    /// 在面板上创建一个按钮
    /// <para>Create a ribbon push button on the panel</para>
    /// </summary>
    /// <typeparam name="T">外部命令，必须是一个自定义类型，且继承于<see cref="Autodesk.Revit.UI.IExternalCommand"/>,且必须存在一个无参的构造函数</typeparam>
    /// <param name="panel">要添加按钮的面板</param>
    /// <param name="handle">对按钮的参数进行赋值</param>
    /// <returns>创建的按钮</returns>
    public static PushButton CreatePushButton<T>(this RibbonPanel panel, Action<PushButtonData> handle = null) where T : class, IExternalCommand, new()
    {
        ArgumentNullExceptionUtils.ThrowIfNull(panel);
        return panel.AddItem(CreatePushButtonData<T>(handle)) as PushButton;
    }

    /// <summary>
    /// 在面板上创建一个下拉按钮
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public static PulldownButton CreatePulldownButton(this RibbonPanel panel, string name, string text, Action<PulldownButtonData> handle = null)
    {
        ArgumentNullExceptionUtils.ThrowIfNull(panel);

        PulldownButtonData data = new(name, text);
        handle?.Invoke(data);

        return panel.AddItem(data) as PulldownButton;
    }

    /// <summary>
    /// 在面板上创建一个下拉按钮
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="name"></param>
    /// <param name="text"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public static SplitButton CreateSplitButton(this RibbonPanel panel, string name, string text, Action<SplitButtonData> handle = null)
    {
        ArgumentNullExceptionUtils.ThrowIfNull(panel);

        SplitButtonData data = new(name, text);
        handle?.Invoke(data);

        return panel.AddItem(data) as SplitButton;
    }

    /// <summary>
    /// 在面板上创建一个下拉框
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="name"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public static ComboBox CreateComboBox(this RibbonPanel panel, string name, Action<ComboBoxData> handle = null)
    {
        ArgumentNullExceptionUtils.ThrowIfNull(panel);

        ComboBoxData combo = new(name);
        handle?.Invoke(combo);
        return panel.AddItem(combo) as ComboBox;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pulldownButton"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public static PushButton CreatePushButton<T>(this PulldownButton pulldownButton, Action<PushButtonData> handle = null) where T : class, IExternalCommand, new()
    {
        ArgumentNullExceptionUtils.ThrowIfNull(pulldownButton);

        return pulldownButton.AddPushButton(CreatePushButtonData<T>(handle));
    }
}
