using JPTollCalc.Contracts;

namespace JPTollCalc.Business;

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        var intervalStart = dates[0];
        var totalFee = 0;
        foreach (var date in dates)
        {
            var nextFee = GetTollFee(date, vehicle);
            var tempFee = GetTollFee(intervalStart, vehicle);
            
            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            var minutes = diffInMillies/1000/60;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
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
            case >= 8 and <= 14 when minute is >= 30 and <= 59:
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

    private Boolean IsTollFreeDate(DateTime date)
    {
        var year = date.Year;
        var month = date.Month;
        var day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
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