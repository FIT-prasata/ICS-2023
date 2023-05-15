using CommunityToolkit.Maui.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;

namespace TimeTracker.App.Converters;

public class UserToStringConverter : BaseConverterOneWay<UserListModel, string>
{
    public override string ConvertFrom(UserListModel value, CultureInfo? _) 
        => value?.FullName ?? string.Empty;
    public override string DefaultConvertReturnValue { get; set; } = String.Empty;


}

