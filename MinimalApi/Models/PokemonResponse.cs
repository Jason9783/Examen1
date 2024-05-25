using System.Diagnostics.Contracts;

namespace MyFavPokeMini.Models
{
    public class PokemonResponse
    {
        public string Nombre { get; set; }
        public List<string> Movimientos { get; set; }
        public List<string> Tipo { get; set; }
        public string SpriteUrl { get; set; }
    }

}
