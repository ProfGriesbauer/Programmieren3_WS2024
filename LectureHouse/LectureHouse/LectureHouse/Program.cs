// See https://aka.ms/new-console-template for more information

using LectureHouse;

LectureHouse.House testHaus = new LectureHouse.House();
LectureHouse.House testHaus2 = new LectureHouse.House(77, 66);
LectureHouse.IHouse testItf = testHaus;

LectureHouse.House.GibMirPIAlsFloat();

testHaus.AlleLichterMainRoom = true;
testHaus.AlleLichterMainRoom = false;
testHaus.RaumHinzufuegen(new NormalerRoom());
testHaus.RaumHinzufuegen(new NormalerRoom());
testHaus.RaumHinzufuegen(new NormalerRoom());
testHaus.RaumHinzufuegen(new NormalerRoom());
testHaus.RaumHinzufuegen(new NormalerRoom());

foreach (IRoom r in testHaus.AlleRäume)
{
    Console.WriteLine("Räume: Hello, World! Raum: {0:F}", testHaus.AlleRäume.IndexOf(r));
}

Console.WriteLine("1: Hello, World! StromV: {0:F}", testHaus.GetStromVerbauchInmA());
testHaus.StromVerbrauchInmA = 77;
Console.WriteLine("1.1: Hello, World! StromV: {0:F}", testHaus2.StromVerbrauchInmA);
Console.WriteLine("1.2: Hello, World! SinnlosesDing: {0:F}", testHaus.SinnloserGesamtverbrauch);

Console.WriteLine("2: Hello, World! StromV über ITF: {0:F}", testItf.GibMirDenStromVerbauchInmA());
