﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace GoFileClient.Common
{
    public static class ToastMessage 
    {
        public static void ShowLongAlert(string message)
        {
            DependencyService.Get<IMessage>().LongAlert(message);
        }

        public static void ShowShortAlert(string message)
        {
            DependencyService.Get<IMessage>().ShortAlert(message);
        }
    }
}
