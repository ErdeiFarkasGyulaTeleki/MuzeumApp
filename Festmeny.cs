namespace MuzeumApp
{
    public class Festmeny : Mutargy
    {
        public int KorEvben { get; set; }

        public Festmeny(string _azonosito, int _becsultErtek, int _korEvben) : base(_azonosito, _becsultErtek)
        {
            KorEvben = _korEvben;
        }

        public override int BiztositasDij()
        {
            return base.BiztositasDij() + KorEvben * 500;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Kor: {KorEvben} év";
        }
    }
}
