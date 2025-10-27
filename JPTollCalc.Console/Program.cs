// See https://aka.ms/new-console-template for more information

using JPTollCalc.Business;
using JPTollCalc.Contracts;

var car = new Car();
var motorbike = new Motorbike();

var tollCalc = new TollCalculator();

Console.WriteLine($"Car toll: {tollCalc.GetTollFee(car, [DateTime.Now])}");
Console.WriteLine($"Motorbike toll: {tollCalc.GetTollFee(motorbike, [DateTime.Now])}");