using JPTollCalc.Contracts;
using NUnit.Framework;

namespace JPTollCalc.Business.Tests;

public class TollCalcTests
{
    private readonly TollCalculator _tollCalc = new();

    private readonly Car _car = new();
    private readonly Motorbike _motorbike = new();

    [Test]
    public void CarWeekdaySinglePass8Sek()
    {
        Assert.That(WeekdaySinglePass8Sek(_car).Equals(8), "Weekday single pass 8 sek");
    }
    
    [Test]
    public void CarWeekdaySinglePass13Sek()
    {
        Assert.That(WeekdaySinglePass13Sek(_car).Equals(13), "Weekday single pass 13 sek");
    }
    
    [Test]
    public void CarWeekdaySinglePass18Sek()
    {
        Assert.That(WeekdaySinglePass18Sek(_car).Equals(18), "Weekday single pass 18 sek");
    }
    
    [Test]
    public void CarWeekendSinglePass()
    {
        Assert.That(WeekendSinglePass(_car).Equals(0), "Weekend single pass 0 sek");
    }
    
    [Test]
    public void CarDayBeforeHolidayPass()
    {
        Assert.That(DayBeforeHolidayPass(_car).Equals(0), "Day before holiday pass 0 sek");
    }
    
    [Test]
    public void CarHolidaySinglePass()
    {
        Assert.That(HolidaySinglePass(_car).Equals(0), "Holiday single pass 0 sek");
    }
    
    [Test]
    public void CarJulySinglePass()
    {
        Assert.That(JulySinglePass(_car).Equals(0), "July single pass 0 sek");
    }
    
    [Test]
    public void CarWeekdayMultiPass13Sek()
    {
        Assert.That(WeekdayMultiPass13Sek(_car).Equals(13), "Weekday multi pass 13 sek");
    }
    
    [Test]
    public void CarWeekdayMultiPass16Sek()
    {
        Assert.That(WeekdayMultiPass16Sek(_car).Equals(16), "Weekday multi pass 16 sek");
    }

    [Test]
    public void MotorbikeWeekdaySinglePass8Sek()
    {
        Assert.That(WeekdaySinglePass8Sek(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeWeekdaySinglePass13Sek()
    {
        Assert.That(WeekdaySinglePass13Sek(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeWeekdaySinglePass18Sek()
    {
        Assert.That(WeekdaySinglePass18Sek(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeWWeekendSinglePass()
    {
        Assert.That(WeekendSinglePass(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeDayBeforeHolidayPass()
    {
        Assert.That(DayBeforeHolidayPass(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeHolidaySinglePass()
    {
        Assert.That(HolidaySinglePass(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeJulySinglePass()
    {
        Assert.That(JulySinglePass(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeWeekdayMultiPass13Sek()
    {
        Assert.That(WeekdayMultiPass13Sek(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }
    
    [Test]
    public void MotorbikeWeekdayMultiPass16Sek()
    {
        Assert.That(WeekdayMultiPass16Sek(_motorbike).Equals(0), "Motorbikes should be toll free!");
    }

    private int WeekdaySinglePass8Sek(IVehicle vehicle)
    {
        var dateTime = new DateTime(2025, 10, 27, 6, 0, 0);
        return _tollCalc.GetTollFee(vehicle, [dateTime]);
    }
    
    private int WeekdaySinglePass13Sek(IVehicle vehicle)
    {
        var dateTime = new DateTime(2025, 10, 27, 6, 30, 0);
        return _tollCalc.GetTollFee(vehicle, [dateTime]);
    }
    
    private int WeekdaySinglePass18Sek(IVehicle vehicle)
    {
        var dateTime = new DateTime(2025, 10, 27, 7, 0, 0);
        return _tollCalc.GetTollFee(vehicle, [dateTime]);
    }
    
    private int WeekendSinglePass(IVehicle vehicle)
    {
        var dateTime = new DateTime(2025, 10, 25, 10, 30, 0);
        return _tollCalc.GetTollFee(vehicle, [dateTime]);
    }
    
    private int DayBeforeHolidayPass(IVehicle vehicle)
    {
        // Day before good friday
        var dateTime = new DateTime(2025, 4, 17, 7, 0, 0);
        return _tollCalc.GetTollFee(vehicle, [dateTime]);
    }
    
    private int HolidaySinglePass(IVehicle vehicle)
    {
        var dateTime = new DateTime(2025, 12, 25, 10, 30, 0);
        return _tollCalc.GetTollFee(vehicle, [dateTime]);
    }
    
    private int JulySinglePass(IVehicle vehicle)
    {
        var dateTime = new DateTime(2025, 7, 15, 10, 30, 0);
        return _tollCalc.GetTollFee(vehicle, [dateTime]);
    }
    
    private int WeekdayMultiPass13Sek(IVehicle vehicle)
    {
        var firstPass = new DateTime(2025, 10, 27, 14, 45, 0);
        var secondPass = new DateTime(2025, 10, 27, 15, 10, 0);
        return _tollCalc.GetTollFee(vehicle, [firstPass, secondPass]);
    }
    
    private int WeekdayMultiPass16Sek(IVehicle vehicle)
    {
        var firstPass = new DateTime(2025, 10, 27, 6, 15, 0);
        var secondPass = new DateTime(2025, 10, 27, 10, 10, 0);
        return _tollCalc.GetTollFee(vehicle, [firstPass, secondPass]);
    }
}