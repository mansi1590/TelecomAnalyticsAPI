using System.ComponentModel.DataAnnotations;

namespace TelecomAnalyticsAPI.Models;

public class CdrRecord
{


    public string CallerName { get; set; } = string.Empty;
    public string CallerNumber { get; set; } = string.Empty;

    public string ReceiverNumber { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;

    public int calldirection { get; set; }

    public int Callstatus{ get; set; }

    public int CallDuration { get; set; }

    public double CallCost { get; set; }
 
    public DateTime CallStartTime { get; set; }

    public DateTime CallEndTime { get; set; }
    [Key]
    public int Id { get; set; }

}