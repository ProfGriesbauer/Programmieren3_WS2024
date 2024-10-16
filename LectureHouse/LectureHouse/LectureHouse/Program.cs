// See https://aka.ms/new-console-template for more information

LectureHouse.House testHaus = new LectureHouse.House();
LectureHouse.House testHaus2 = new LectureHouse.House();
LectureHouse.IHouse testItf = testHaus;

Console.WriteLine("1: Hello, World! StromV: {0:F}", testHaus.GetStromVerbauchInmA());
testHaus.StromVerbrauchInmA = 77;
Console.WriteLine("1.1: Hello, World! StromV: {0:F}", testHaus.StromVerbrauchInmA);
Console.WriteLine("1.1: Hello, World! SinnlosesDing: {0:F}", testHaus.SinnloserGesamtverbrauch);

Console.WriteLine("2: Hello, World! StromV: {0:F}", testItf.GibMirDenStromVerbauchInmA());
