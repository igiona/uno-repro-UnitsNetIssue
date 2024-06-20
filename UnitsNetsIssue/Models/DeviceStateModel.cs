using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace UnitsNetsIssue.Models;
public record DeviceStateModel(
    bool OperationIsInProgress,
    //int? BatteryStateOfCharge,
    Ratio? BatteryStateOfCharge
);
