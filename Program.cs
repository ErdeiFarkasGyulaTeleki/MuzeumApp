using MuzeumApp;

List<Mutargy> mutargyak = new List<Mutargy>();

int hibasSorokSzama = 0;

try
{
    string[] sorok = File.ReadAllLines("mutargyak.txt").Skip(1).ToArray();

    foreach (string sor in sorok)
    {
        if (string.IsNullOrWhiteSpace(sor))
        {
            hibasSorokSzama++;
            continue;
        }

        string[] adatok = sor.Split('|');

        if (adatok.Length != 4)
        {
            hibasSorokSzama++;
            continue;
        }

        string tipus = adatok[0];
        if (!(tipus == "M" || tipus == "F"))
        {
            hibasSorokSzama++;
            continue;
        }

        string azonosito = adatok[1];
        if (string.IsNullOrWhiteSpace(azonosito))
        {
            hibasSorokSzama++;
            continue;
        }

        int becsultErtek = 0;
        if (!int.TryParse(adatok[2], out becsultErtek))
        {
            hibasSorokSzama++;
            continue;
        }

        int extra = 0;
        if (!int.TryParse(adatok[3], out extra))
        {
            hibasSorokSzama++;
            continue;
        }

        if (tipus == "M")
        {
            Mutargy mutargy = new Mutargy(azonosito, becsultErtek);
            mutargyak.Add(mutargy);
        }
        else if (tipus == "F")
        {
            Festmeny festmeny = new Festmeny(azonosito, becsultErtek, extra);
            mutargyak.Add(festmeny);
        }
    }
}
catch (Exception exc)
{
    Console.WriteLine(exc);
}

Console.WriteLine($"Sikeresen beolvasott műtárgyak: {mutargyak.Count} db");
Console.WriteLine($"Kiszűrt hibás sorok száma: {hibasSorokSzama} db");

Console.WriteLine($"\nBeolvasott műtárgyak listája:");
mutargyak.ForEach(Console.WriteLine);

Console.WriteLine("\nLINQ Statisztikák:");
Console.WriteLine($"Műtárgyak átlagos értéke: {mutargyak.Average(m => m.BecsultErtek)} Ft");

var legertekesebb = mutargyak.MaxBy(t => t.BecsultErtek);
if (legertekesebb != null)
{
    Console.WriteLine($"Legértékesebb műtárgy: {legertekesebb.Azonosito} ({legertekesebb.BecsultErtek} Ft)");
}

Console.WriteLine($"Milliós értéket meghaladó műtárgyak: {mutargyak.Count(m => m.BecsultErtek > 1_000_000)} db");

Console.WriteLine("\nFestmények külön elemzése:");
mutargyak.ForEach((m) =>
{
    if (m is Festmeny f)
    {
        Console.WriteLine($"- {f.Azonosito} festmény kora: {f.KorEvben} év.");
    }
});


try
{
    List<Mutargy> rendezett = mutargyak.OrderByDescending(m => m.BecsultErtek).ToList();

    string[] formazottSorok = rendezett.Select((Mutargy m, int i) =>
    {
        return $"{m.Azonosito};{m.BecsultErtek};{m.BiztositasDij()}";
    }).ToArray();

    File.WriteAllLines("riport.txt", formazottSorok);

    Console.WriteLine("\nA riport.txt fájl sikeresen elkészült a kimeneti mappában.");

} catch (Exception exc)
{
    Console.WriteLine(exc);
}

Console.WriteLine($"Műtárgy példányok száma (static): {Mutargy.OsszesMutargySzama} db");