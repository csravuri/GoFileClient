using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GoFileClient.Common
{
    public static class Utils
    {
        
        public static void MakeSureDirectoryExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public static bool IsNumber(string text, bool allowNullOrEmpty = true)
        {
            if (string.IsNullOrEmpty(text))
            {
                return allowNullOrEmpty;
            }

            if (text.All(x => (x >= 48 || x == 46) && (x <= 57 || x == 46)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string HumanReadableFileSize(long sizeInBytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (sizeInBytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                sizeInBytes = sizeInBytes / 1024;
            }

            return $"{sizeInBytes:0.##} {sizes[order]}";
        }

        public static decimal ToDecimal(string text, decimal defaultValue = 0)
        {
            decimal outValue;
            if (decimal.TryParse(text, out outValue))
            {
                return outValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public static string ToString(decimal decimalValue, string defaultValue = null)
        {
            if (decimalValue == 0)
            {
                return defaultValue;
            }
            else
            {
                return decimalValue.ToString();
            }
        }
        public static string ToString(string text, string defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return defaultValue;
            }
            else
            {
                return text.ToString();
            }
        }
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> list)
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            
            list.ForEach(x => observableCollection.Add(x));
        }
    }
}
