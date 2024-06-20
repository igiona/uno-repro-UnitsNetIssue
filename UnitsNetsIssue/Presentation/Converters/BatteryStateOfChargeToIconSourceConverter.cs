using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;
using UnitsNet;

namespace UnitsNetsIssue.Presentation.Converters;
internal static class Extensions
{
    internal const string IconBaseTemplate = "ms-appx:///Assets/Images/battery_{0}.png";
    internal static readonly string IconLevelTemplate = string.Format(IconBaseTemplate, "{0}_bar");
    internal const int BatteryLevels = 7;

    static readonly Ratio PercentLevelSteps = Ratio.FromPercent(100.0 / (double)BatteryLevels);
    //static readonly int PercentLevelSteps = (int)(100.0 / (double)BatteryLevels);

    internal static int? ToBatteryLevel(this Ratio? bsoc)
    {
        if (bsoc.HasValue)
        {
            int i;
            var bsoc_threshold = PercentLevelSteps;
            for (i = 1; i < BatteryLevels; i++, bsoc_threshold += PercentLevelSteps)
            {
                if (bsoc < bsoc_threshold)
                {
                    break;
                }
            }
            return i;
        }
        return null;
    }
}

public class BatteryStateOfChargeToIconSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var level = (value as Ratio?).ToBatteryLevel();
        //var level = (value as int?).ToBatteryLevel();
        if (level.HasValue)
        {
            return string.Format(Extensions.IconLevelTemplate, level);
        }
        return string.Format(Extensions.IconBaseTemplate, "unknown");
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
