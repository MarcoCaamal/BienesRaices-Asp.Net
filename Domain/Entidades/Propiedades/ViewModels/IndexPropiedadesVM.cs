namespace Domain.Entidades.Propiedades.ViewModels
{
    public class IndexPropiedadesVM
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
    }
}
