namespace ArticulosApi.Models
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public override string? ToString()
        {
            return Nombre + "[" + IdArticulo + "]";
        }
    }
}
