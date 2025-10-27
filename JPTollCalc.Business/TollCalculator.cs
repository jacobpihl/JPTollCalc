using JPTollCalc.Contracts;
using PublicHoliday;

namespace JPTollCalc.Business;

public class TollCalculator
{

    private const int MaximumFee = 60;
    
    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        var orderedDates = dates.OrderBy(d => d).ToList();
        var intervalStart = orderedDates[0];
        var totalFee = 0;
        foreach (var date in orderedDates)
        {
            var nextFee = GetTollFee(date, vehicle);
            var diffInMinutes = date.Subtract(intervalStart).TotalMinutes;
            if (diffInMinutes > 60)
            {
                totalFee += nextFee;
                continue;
            }
            
            var tempFee = GetTollFee(intervalStart, vehicle);
                
            if (totalFee > 0) totalFee -= tempFee;
            if (nextFee >= tempFee) tempFee = nextFee;
            totalFee += tempFee;
        }
        
        return totalFee > MaximumFee
            ? MaximumFee
            : totalFee;
    }

    private int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        var hour = date.Hour;
        var minute = date.Minute;

        switch (hour)
        {
            case 6 when minute is >= 0 and <= 29:
                return 8;
            case 6 when minute is >= 30 and <= 59:
                return 13;
            case 7 when minute is >= 0 and <= 59:
                return 18;
            case 8 when minute is >= 0 and <= 29:
                return 13;
            case 8 when minute is >= 30 and <= 59:
            case >= 8 and <= 14:
                return 8;
            case 15 when minute is >= 0 and <= 29:
                return 13;
            case 15 when minute >= 30:
            case 16 when minute is >= 0 and <= 59:
                return 18;
            case 17 when minute is >= 0 and <= 59:
                return 13;
            case 18 when minute is >= 0 and <= 29:
                return 8;
            default:
                return 0;
        }
    }

    private static bool IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) return true;

        // July is toll-free
        if (date.Month == 7) return true;

        var nextDay = date.AddDays(1);
        return IsHolidayDate(date) || IsHolidayDate(nextDay);
    }

    private static bool IsHolidayDate(DateTime date)
    {
        // TODO: Probably use a trusted source for the holidays rather than a random Nuget package :)
        var holidayChecker = new SwedenPublicHoliday();
        return holidayChecker.IsPublicHoliday(date);
    }
    
    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        return _tollFreeVehicles.Contains(vehicle.VehicleType);
    }

    private readonly List<VehicleType> _tollFreeVehicles =
    [
        VehicleType.Motorbike,
        VehicleType.Tractor,
        VehicleType.Emergency,
        VehicleType.Diplomat,
        VehicleType.Foreign,
        VehicleType.Military
    ];
}