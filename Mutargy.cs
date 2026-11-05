namespace MuzeumApp
{
    public class Mutargy
    {
        public string Azonosito { get; set; }

        protected int becsultErtek;
        public int BecsultErtek
        {
            get {  return becsultErtek; }
            set
            {
                becsultErtek = value > 0 ? value : 50000;
            }
        }

        private static int osszesMutargySzama;
        public static int OsszesMutargySzama
        {
            get { return osszesMutargySzama; }
        }

        public Mutargy(string _azonosito, int _becsultErtek)
        {
            Azonosito = _azonosito;
            BecsultErtek = _becsultErtek;

            osszesMutargySzama++;
        }

        public virtual int BiztositasDij()
        {
            return (int)(BecsultErtek * 0.02);
        }

        public override string ToString()
        {
            return $"Műtárgy {Azonosito} | Becsült érték: {BecsultErtek} Ft | Biztosítás: {BiztositasDij()} Ft";
        }
    }
}
